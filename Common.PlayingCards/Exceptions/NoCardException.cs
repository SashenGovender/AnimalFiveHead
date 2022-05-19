using System;

namespace Common.PlayingCards.Exceptions
{
#pragma warning disable S3925
  public class NoCardException : Exception
  {
    public NoCardException() : base("No Cards in Card Deck")
    {

    }
  }
}
