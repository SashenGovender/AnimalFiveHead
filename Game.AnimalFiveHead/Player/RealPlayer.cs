using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;

namespace Game.AnimalFiveHead.Player
{
  public class RealPlayer : BasePlayer
  {
    public RealPlayer(int playerId) : base(playerId, PlayCardFace.None)
    {
    }

    public override void Chain(Func<PlayCard> getCard)
    {
      if (Cards.Count < 5)
      {
        var card = getCard.Invoke();
        Cards.Add(card);
      }
    }
  }
}
