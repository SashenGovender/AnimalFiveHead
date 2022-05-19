using Common.DatabaseAccess.Extensions;
using Game.AnimalFive.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NoName.GameplayApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();

      services.AddDapperDatabaseAccess();
      services.AddAnimalFiveGame();
      //services.Configure<DatabaseConnectionInformation>(Configuration.GetSection("AnimalFiveDatabaseConnectionInformation"))

      //services
      //    .AddControllers()
      //    .AddJsonOptions(options =>
      //        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
      //    );

      //services.AddCors(options =>
      //{
      //    options.AddPolicy("sashen",
      //    builder =>
      //    {
      //        builder.WithOrigins("file:///D:/Downloads/Programming%20Learning/Projects/IntroductionToCoreWebAPI/game.html");

      //    });
      //});
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

      //app.UseCors("sashen");

      app.UseHttpsRedirection();
      app.UseFileServer();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "api/{controller=Values}/{action=Index}/{id?}");
      });
    }
  }
}
