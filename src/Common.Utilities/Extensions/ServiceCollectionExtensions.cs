using Common.Utilities.Randomizer;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Utilities.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddRandomNumberGenerator(this IServiceCollection services)
    {
      services.AddSingleton<IRandomNumberGenerator, StockRandomNumberGenerator>();

      return services;
    }
  }
}
