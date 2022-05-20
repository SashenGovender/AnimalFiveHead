using System;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;

namespace Game.AnimalFiveHead.Player
{
  public class NormalPlayer : BasePlayer
  {
    public NormalPlayer(int playerId) : base(playerId, PlayCardFace.None)
    {
    }

    public override void Chain(Func<PlayCard> getCard)
    {
      if (Cards.Count < 6)
      {
        var card = getCard.Invoke();
        Cards.Add(card);
      }
    }
  }
}
