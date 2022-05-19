using System.Collections.Generic;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public interface IPlayer
  {
    public void AddCard(PlayCard card);
    public int Score { get; }
    public int PlayerId { get; }
    public List<PlayCard> Cards { get; }
    public PlayerGameResult GameStatus { get; set; }
  }
}
