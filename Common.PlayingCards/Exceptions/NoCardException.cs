using System;
using System.Runtime.Serialization;

namespace Common.PlayingCards.Exceptions
{
  [Serializable]
  public class NoCardException : Exception
  {
    public NoCardException() : base("No more cards in playing deck")
    {

    }

    public NoCardException(string? message) : base(message)
    {
    }

    public NoCardException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NoCardException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
