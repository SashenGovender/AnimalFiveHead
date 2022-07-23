using Common.DatabaseDapperAccess.ConnectionProviders;
using Database.Entity.AnimalFiveHead;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DatabaseDapperAccess.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDapperDatabaseAccess(this IServiceCollection services)
    {
      services.AddSingleton<IDatabaseConnectionProvider, SqlConnectionProvider>();
      services.AddSingleton<IDatabaseDapperAccess, DapperDatabaseAccess>();

      return services;
    }

    public static IServiceCollection AddEntityFrameworkAccess(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AnimalFiveHeadContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("PezzaDatabase")));

      return services;
    }
  }
}
