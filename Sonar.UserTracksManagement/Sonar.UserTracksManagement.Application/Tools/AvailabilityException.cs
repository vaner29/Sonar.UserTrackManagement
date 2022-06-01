using System.Runtime.Serialization;

namespace Sonar.UserTracksManagement.Application.Tools;

public class AvailabilityException : Exception
{
    public AvailabilityException()
        : base()
    {
    }

    public AvailabilityException(string message)
        : base(message)
    {
    }

    public AvailabilityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public AvailabilityException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}