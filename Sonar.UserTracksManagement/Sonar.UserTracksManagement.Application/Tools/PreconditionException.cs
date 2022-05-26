namespace Sonar.UserTracksManagement.Application.Tools;

public class PreconditionException : Exception
{
    public PreconditionException()
        : base()
    {
    }

    public PreconditionException(string message)
        : base(message)
    {
    }

    public PreconditionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}