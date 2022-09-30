using Common.DatabaseDapperAccess.ConnectionProviders;
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
  }
}
