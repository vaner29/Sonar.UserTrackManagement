using System.Runtime.Serialization;

namespace Sonar.UserTracksManagement.Application.Tools;

public class UserAccessException : Exception
{
    public UserAccessException()
        : base()
    {
    }

    public UserAccessException(string message)
        : base(message)
    {
    }

    public UserAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
    
    public UserAccessException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}