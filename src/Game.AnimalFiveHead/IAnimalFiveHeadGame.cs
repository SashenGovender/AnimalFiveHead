using System.Collections.Generic;
using Common.PlayingCards.CardDecks;
using Game.AnimalFiveHead.Player;

namespace Game.AnimalFiveHead
{
  public interface IAnimalFiveHeadGame
  {
    public List<BasePlayer> NpcPlayers { get; }
    public List<BasePlayer> RealPlayers { get; }
    public IDeck? CardDeck { get; }

    public void InitialiseDeck(IDeck cardDeck);
    public void AddNpcPlayers();
    public void AddRealPlayers(int numberOfPlayers);
    public void BeginGame();
    public BasePlayer Chain(int playerId);
    public void PlayNpcRound();
    public void EndGame();
  }
}
