using System.Runtime.Serialization;

namespace Sonar.UserTracksManagement.Application.Tools;

public class InvalidArgumentsException : Exception
{
    public InvalidArgumentsException()
        : base()
    {
    }

    public InvalidArgumentsException(string message)
        : base(message)
    {
    }

    public InvalidArgumentsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public InvalidArgumentsException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}