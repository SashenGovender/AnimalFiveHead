using System;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Exceptions;

namespace Common.PlayingCards
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
      var deck = type switch
      {
        DeckType.Standard => (IDeck)_serviceProvider.GetService(typeof(StandardDeck)),
        DeckType.AnimalFiveHead => (IDeck)_serviceProvider.GetService(typeof(AnimalFiveHeadDeck)),
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
