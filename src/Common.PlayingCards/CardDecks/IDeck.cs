using System.Collections.Generic;
using Common.PlayingCards.Models;

namespace Common.PlayingCards.CardDecks
{
  //Design: Do i need this interface as i am using an abstract class
  public interface IDeck
  {
    public void AddCard(PlayCard card);
    public PlayCard? GetCard();
    public PlayCard? GetCard(int id);
    public List<PlayCard> Cards { get; }
    public void Shuffle();
  }
}
