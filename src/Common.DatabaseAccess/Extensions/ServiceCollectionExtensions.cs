using Common.DatabaseAccess.ConnectionProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DatabaseAccess.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDapperDatabaseAccess(this IServiceCollection services)
    {
      services.AddSingleton<IDatabaseConnectionProvider, SqlConnectionProvider>();
      services.AddSingleton<IDatabaseAccess, DapperDatabaseAccess>();

      return services;
    }
  }
}