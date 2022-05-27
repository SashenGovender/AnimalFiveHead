using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public class NpcKeeperPlayer : BasePlayer
  {
    public NpcKeeperPlayer() : base(AnimalFiveHeadConstants.KeeperId, PlayCardFace.HoneyBee)
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
