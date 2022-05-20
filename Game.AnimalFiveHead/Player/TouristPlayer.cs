using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;

namespace Game.AnimalFiveHead.Player
{
  public class TouristPlayer : BasePlayer
  {
    public TouristPlayer() : base(AnimalFiveHeadConstants.TouristId, PlayCardFace.Rabbit)
    {
    }

    public override void Chain(Func<PlayCard> getCard)
    {
      while (Cards.Count < 4)
      {
        var card = getCard.Invoke();
        Cards.Add(card);
      }
    }
  }
}
