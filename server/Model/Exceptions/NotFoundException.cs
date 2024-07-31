namespace ping_Map_Play_pong.Model.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message, 404)
    {
    }
}