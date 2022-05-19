using Common.PlayingCards.Enums;

namespace Game.AnimalFive.Player
{
  public class KeeperPlayer : NormalPlayer
  {
    public KeeperPlayer() : base(AnimalFiveHeadConstants.KeeperId, CardFace.HoneyBee)
    {
    }
  }
}
