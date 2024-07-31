namespace ping_Map_Play_pong.Model.Exceptions;

public class UnauthorizedException : ExceptionBase
{
    public UnauthorizedException(string message, int statusCode) : base(message, 401)
    {
    }
}