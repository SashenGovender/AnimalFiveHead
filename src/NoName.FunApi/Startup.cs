using System.Text.Json.Serialization;
using Common.DatabaseAccess.Extensions;
using Game.AnimalFiveHead.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoName.FunApi.DataAccess;
using NoName.FunApi.GameManager;

namespace NoName.FunApi
{
  public class Startup
  {
    private const string CORSPolicyName = "NoNameFunApi";
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.
        AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();

      services.AddDapperDatabaseAccess();
      services.AddAnimalFiveGame();

      services.AddSingleton<IAnimalFiveDatabaseAccess, AnimalFiveDatabaseAccess>();
      services.AddTransient<IAnimalFiveManager, AnimalFiveManager>();
      services.AddTransient<IAnimalFiveDatabaseSessionManager, AnimalFiveDatabaseSessionManager>();


      //https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
#pragma warning disable IDE0053 // Use expression body for lambda expressions
      services.AddCors(options =>
      {
        options.AddPolicy(CORSPolicyName, builder =>
        {
          builder.AllowAnyMethod();
          builder.AllowAnyHeader();
          builder.AllowCredentials();
          builder.WithOrigins("http://https://localhost:7001/html/welcome");

        });
      });
#pragma warning restore IDE0053 // Use expression body for lambda expressions

      //services.Configure<DatabaseConnectionInformation>(Configuration.GetSection("AnimalFiveDatabaseConnectionInformation"))

      //services
      //    .AddControllers()
      //    .AddJsonOptions(options =>
      //        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
      //    );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors(CORSPolicyName);

      app.UseHttpsRedirection();
      app.UseFileServer();

      app.UseRouting();

      app.UseAuthorization();

#pragma warning disable IDE0053 // Use expression body for lambda expressions
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "api/{controller=Values}/{action=Index}/{id?}");
      });
#pragma warning restore IDE0053 // Use expression body for lambda expressions
    }
  }
}
