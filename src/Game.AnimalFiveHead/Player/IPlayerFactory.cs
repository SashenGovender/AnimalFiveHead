using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public interface IPlayerFactory
  {
    public BasePlayer GetNpcPlayer(NpcPlayerType playerType);
    public BasePlayer GetRealPlayer(int playerId);
  }
}
