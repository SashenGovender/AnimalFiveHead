using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Exceptions;

namespace Common.PlayingCards.CardDecks
{
  public class DeckFactory
  {
    private readonly IServiceProvider _serviceProvider;
    public DeckFactory(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    public IDeck CreateDeck(DeckType type)
    {
      var deck = type switch
      {
        DeckType.Standard => _serviceProvider.GetService(typeof(StandardDeck)) as IDeck,
        DeckType.AnimalFiveHead => _serviceProvider.GetService(typeof(AnimalFiveHeadDeck)) as IDeck,
        _ => null
      };

      if (deck == null)
      {
        throw new DeckNotFoundException();
      }

      return deck;
    }
  }
}
