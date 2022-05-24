namespace Sonar.UserTracksManagement.Application.Tools;

public class UserAuthorizationException : Exception
{
    public UserAuthorizationException()
        : base()
    {
    }

    public UserAuthorizationException(string message)
        : base(message)
    {
    }

    public UserAuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}