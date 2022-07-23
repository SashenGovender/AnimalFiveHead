using System;
using System.Runtime.Serialization;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Exceptions
{
  [Serializable]
  public class InvalidPlayerTypeException : Exception
  {
    public InvalidPlayerTypeException()
    {
    }

    public InvalidPlayerTypeException(NpcPlayerType playerType) : base($"Invalid PlayerType: {playerType}")
    {
    }

    public InvalidPlayerTypeException(string? message) : base(message)
    {
    }

    public InvalidPlayerTypeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidPlayerTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
