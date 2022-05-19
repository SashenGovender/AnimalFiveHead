using Common.PlayingCards.Enums;

namespace Game.AnimalFiveHead.Player
{
  public class KeeperPlayer : NormalPlayer
  {
    public KeeperPlayer() : base(AnimalFiveHeadConstants.KeeperId, PlayCardFace.HoneyBee)
    {
    }
  }
}
