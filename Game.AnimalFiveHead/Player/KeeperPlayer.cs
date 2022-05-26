using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;

namespace Game.AnimalFiveHead.Player
{
  public class KeeperPlayer : BasePlayer
  {
    public KeeperPlayer() : base(AnimalFiveHeadConstants.KeeperId, PlayCardFace.HoneyBee)
    {
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
