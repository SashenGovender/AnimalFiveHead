namespace Game.AnimalFiveHead.Player
{
  public class PlayerFactory : IPlayerFactory
  {
#pragma warning disable CA1822 // Mark members as static
    public BasePlayer GetPlayer(int playerId)
#pragma warning restore CA1822 // Mark members as static
    {
      BasePlayer player = playerId switch
      {
        AnimalFiveHeadConstants.KeeperId => new KeeperPlayer(),
        AnimalFiveHeadConstants.TouristId => new TouristPlayer(),
        _ => new NormalPlayer(playerId),
      };

      return player;
    }

  }
}
