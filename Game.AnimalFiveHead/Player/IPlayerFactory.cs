namespace Game.AnimalFiveHead.Player
{
  public interface IPlayerFactory
  {
    public BasePlayer GetPlayer(int playerId);
  }
}
