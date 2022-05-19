using System.Collections.Generic;
using Common.PlayingCards.Models;

namespace Common.PlayingCards.CardDecks
{
  public interface IDeck
  {
    public void AddCard(Card card);
    public Card? GetCard();
    public Card? GetCard(int id);
    public List<Card> Cards { get; }
    public void Shuffle();
  }
}
