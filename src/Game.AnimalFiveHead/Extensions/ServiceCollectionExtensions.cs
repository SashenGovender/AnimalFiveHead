using Common.PlayingCards.Extensions;
using Game.AnimalFiveHead.Player;
using Microsoft.Extensions.DependencyInjection;

namespace Game.AnimalFiveHead.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddAnimalFiveGame(this IServiceCollection services)
    {
      services.AddTransient<IAnimalFiveHeadGame, AnimalFiveHeadGame>();
      services.AddSingleton<IPlayerFactory, PlayerFactory>();

      services.AddPlayingDeck();

      return services;
    }
  }
}
