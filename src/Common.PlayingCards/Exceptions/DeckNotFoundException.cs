using System;
using System.Runtime.Serialization;
using Common.PlayingCards.Enums;

namespace Common.PlayingCards.Exceptions
{
  [Serializable]
  public class DeckNotFoundException : Exception
  {
    public DeckNotFoundException()
    {
    }

    public DeckNotFoundException(DeckType deckType) : base($"Cannot find {deckType} type")
    {
    }

    public DeckNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected DeckNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
