using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public class NpcTouristPlayer : BasePlayer
  {
    public NpcTouristPlayer() : base((int)NpcPlayerType.TouristId, PlayCardFace.Rabbit)
    {
      GameStatus = PlayerMatchResult.NoResult;
    }

    public override void Chain(Func<PlayCard> getCard)
    {
      while (Cards.Count < 5)
      {
        var card = getCard.Invoke();
        Cards.Add(card);
      }
    }
  }
}
