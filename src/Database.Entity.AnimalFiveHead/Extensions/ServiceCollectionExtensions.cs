using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Entity.AnimalFiveHead.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddEntityFrameworkAccess(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AnimalFiveHeadContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("PezzaDatabase")));

      return services;
    }
  }
}
