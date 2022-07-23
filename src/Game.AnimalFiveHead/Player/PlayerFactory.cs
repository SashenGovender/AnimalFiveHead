using Game.AnimalFiveHead.Enums;
using Game.AnimalFiveHead.Exceptions;

namespace Game.AnimalFiveHead.Player
{
  public class PlayerFactory : IPlayerFactory
  {
    public BasePlayer GetNpcPlayer(NpcPlayerType playerType)
    {
      BasePlayer player = playerType switch
      {
        NpcPlayerType.KeeperId => new NpcKeeperPlayer(),
        NpcPlayerType.TouristId => new NpcTouristPlayer(),
        NpcPlayerType.NpcPlayerStartRange => throw new InvalidPlayerTypeException(playerType),
        NpcPlayerType.NpcPlayerEndRange => throw new InvalidPlayerTypeException(playerType),
        _ => throw new InvalidPlayerTypeException(playerType)
      };

      return player;
    }

    public BasePlayer GetRealPlayer(int playerId) => new RealPlayer(playerId);
  }
}
