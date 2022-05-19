using Common.PlayingCards.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Game.AnimalFiveHead.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddAnimalFiveGame(this IServiceCollection services)
    {
      services.AddSingleton<IAnimalFive, AnimalFiveHead>();
      services.AddPlayingDeck();

      return services;
    }
  }
}
