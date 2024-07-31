using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/tables")]

public class TableController : ControllerBase
{
    private readonly ILogger<TableController> _logger;
    private readonly ITableService _tableService;

    public TableController(ILogger<TableController> logger, ITableService tableService)
    {
        _logger = logger;
        _tableService = tableService;
    }

    [HttpGet(Name = "tables")]
    public ActionResult<IEnumerable<TableResponse>> GetAll()
    {
        try
        {
            return Ok(_tableService.GetAll());
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpGet("{tableId}")]
    public ActionResult<Table> GetById(int tableId)
    {
        try
        {
            return Ok(_tableService.GetById(tableId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpPost("add")]
    public IActionResult Post([FromBody] TableRequest request)
    {
        try
        { 
            _tableService.PostToDb(request);
            return Ok(new { message = "success registering" });
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpPatch("update/{tableId}")]
    public IActionResult Update(int tableId, [FromBody] TableRequest request)
    {
        try
        {
            _tableService.Update(tableId, request);
            return Ok(new { message = "Successful update" });
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpDelete("delete/{tableId}")]
    public ActionResult<string> Delete(int tableId)
    {
        try
        {
            _tableService.Delete(tableId);
            return Ok("successful delete");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
}