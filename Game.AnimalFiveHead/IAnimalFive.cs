using System.Collections.Generic;
using Common.PlayingCards.CardDecks;
using Game.AnimalFiveHead.Player;

namespace Game.AnimalFiveHead
{
  public interface IAnimalFive
  {
    public BasePlayer Tourist { get; }
    public BasePlayer Keeper { get; }
    public List<BasePlayer> Players { get; }
    public IDeck? CardDeck { get; }

    public void InitialiseDeck(IDeck cardDeck);
    public void BeginGame();
    public BasePlayer Chain(int playerId);
    public void PlayNpcRound();
    public void EndGame();
    void AddRealPlayers(int numberOfPlayers);
  }
}
