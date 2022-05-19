using System;

namespace Game.AnimalFiveHead.Exceptions
{
#pragma warning disable S3925
  public class NoCardException : Exception
  {
    public NoCardException() : base("No Cards in Card Deck")
    {

    }
  }
}
