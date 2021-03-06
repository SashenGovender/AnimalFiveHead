using Common.PlayingCards.CardDecks;
using Common.Utilities.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Common.PlayingCards.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddPlayingDeck(this IServiceCollection services)
    {
      services.AddSingleton<DeckFactory>();
      services.AddTransient<StandardDeck>();
      services.AddTransient<AnimalFiveHeadDeck>();

      services.AddRandomNumberGenerator();

      return services;
    }
  }
}
