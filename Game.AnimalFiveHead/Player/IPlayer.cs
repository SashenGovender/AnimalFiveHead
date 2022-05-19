using System.Collections.Generic;
using Common.PlayingCards.Models;
using Game.AnimalFive.Enums;

namespace Game.AnimalFive.Player
{
  public interface IPlayer
  {
    public void AddCard(Card card);
    public int Score { get; }
    public int PlayerId { get; }
    public List<Card> Cards { get; }
    public GameStatus GameStatus { get; set; }
  }
}
