using System.Collections.Generic;
using Common.PlayingCards.CardDecks;
using Game.AnimalFiveHead.Player;

namespace Game.AnimalFiveHead
{
  public interface IAnimalFive
  {
    public void Initialise(IDeck cardDeck, List<NormalPlayer> players, KeeperPlayer keeper, TouristPlayer tourist);
    public void BeginPlay();
    public IPlayer Chain(int playerId);
    public void CompletePlayerGame(IPlayer player);
    public TouristPlayer Tourist { get; }
    public KeeperPlayer Keeper { get; }
    public List<NormalPlayer> Players { get; }
    public IDeck CardDeck { get; }
  }
}
