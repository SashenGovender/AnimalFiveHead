using System;
using System.Runtime.Serialization;

namespace Common.DatabaseDapperAccess.Exceptions
{
  [Serializable]
  public class DatabaseException : Exception
  {
    public DatabaseException()
    {
    }

    public DatabaseException(string? action) : base($"Failed to update Database when performing {action}")
    {
    }

    public DatabaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
