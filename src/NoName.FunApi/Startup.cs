using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Common.DatabaseDapperAccess.Extensions;
using Common.Models.Profiles;
using Game.AnimalFiveHead.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NoName.FunApi.DataAccess;
using NoName.FunApi.Middleware;
using NoName.FunApi.Services;
using NoName.FunApi.SessionManager;

namespace NoName.FunApi
{
  public class Startup
  {
    private const string CORSPolicyName = "NoNameFunApi";

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.
        AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "NoName Fun API",
          Version = "v1"
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });

      //https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0
      services.AddHealthChecks()
        .AddSqlServer(Configuration.GetConnectionString("AnimalFiveHead"));

      //https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
      services.AddCors(options => options.AddPolicy(CORSPolicyName, builder =>
        {
          builder.AllowAnyMethod();
          builder.AllowAnyHeader();
          builder.AllowCredentials();
          builder.WithOrigins("http://https://localhost:7001/html/welcome");
        }));

      services.AddDapperDatabaseAccess();
      services.AddAnimalFiveGame();

      services.AddSingleton<IAnimalFiveDatabaseAccess, AnimalFiveHeadDapperDatabaseAccess>();
      services.AddTransient<IAnimalFiveHeadService, AnimalFiveHeadService>();
      services.AddTransient<IAnimalFiveHeadSessionManager, AnimalFiveHeadDatabaseSessionManager>();

      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddAutoMapper(typeof(MappingProfile));

      //services.Configure<DatabaseConnectionInformation>(Configuration.GetSection("AnimalFiveDatabaseConnectionInformation"))
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoName Fun API");
        });

        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

      app.UseCors(CORSPolicyName);

      app.UseHttpsRedirection();
      app.UseFileServer();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/health");
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "api/{controller=Values}/{action=Index}/{id?}");
      });

    }
  }
}
