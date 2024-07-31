namespace ping_Map_Play_pong.Model.Exceptions;

public class BadRequestException : ExceptionBase
{
    public BadRequestException(string message) : base(message, 400)
    {
    }
}