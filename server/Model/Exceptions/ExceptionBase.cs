using Microsoft.AspNetCore.Mvc;

namespace ping_Map_Play_pong.Model.Exceptions;

public abstract class ExceptionBase : Exception
{
    private int StatusCode { get; }

    protected ExceptionBase(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
    public ObjectResult GetResponse(string message)
    {
        return StatusCode switch
        {
            400 => new BadRequestObjectResult(message),
            401 => new UnauthorizedObjectResult(message),
            404 => new NotFoundObjectResult(message),
            409 => new ConflictObjectResult(message),
            _ => new ObjectResult("default")
        };
    }
}