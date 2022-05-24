namespace Sonar.UserTracksManagement.Application.Tools;

public class NotFoundArgumentsException : Exception
{
    public NotFoundArgumentsException()
        : base()
    {
    }

    public NotFoundArgumentsException(string message)
        : base(message)
    {
    }

    public NotFoundArgumentsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}