namespace Game.AnimalFiveHead.Player
{
  public class PlayerFactory : IPlayerFactory
  {

    public BasePlayer GetPlayer(int playerId)
    {
      BasePlayer player = playerId switch
      {
        AnimalFiveHeadConstants.KeeperId => new NpcKeeperPlayer(),
        AnimalFiveHeadConstants.TouristId => new NpcTouristPlayer(),
        _ => new RealPlayer(playerId),
      };

      return player;
    }

  }
}
