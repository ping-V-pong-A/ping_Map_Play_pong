using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/tables")]

public class TableController : ControllerBase
{
    private readonly ILogger<TableController> _logger;
    private readonly ITableRepository _tableRepository;

    public TableController(ILogger<TableController> logger, ITableRepository tableRepository)
    {
        _logger = logger;
        _tableRepository = tableRepository;
    }

    [HttpGet(Name = "tables")]
    public ActionResult<IEnumerable<TableResponse>> GetAll()
    {
        try
        {
            var tables = _tableRepository.GetAll();
            var respTables = tables.Select(table => new TableResponse
            {
                Id = table.Id,
                Name = table.Name,
                Lat = table.Coordinate.Lat,
                Lon = table.Coordinate.Lon,
                CheckingIns = table.CheckingIns.ToList(),
                Matches = table.LeaderBoard.ToList(),
                PairMatches = table.PairMatchesLeaderBoard.ToList()
            }).ToList();

            return Ok(respTables);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return NotFound("tables table is empty");
        }
    }

    [HttpGet("{tableId}")]
    public ActionResult<Table> GetById(int tableId)
    {
        try
        {
            return Ok(_tableRepository.GetByTableId(tableId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"table with id:{tableId} not exist in DB");
        }
    }
    
    [HttpPost("add")]
    public IActionResult Post([FromBody] TableRequest request)
    {
        try
        {  
            var coordinate = new Coordinate
            {
                Lat = request.Lat,
                Lon = request.Lon
            };
        
            var table = new Table
            {
                Name = request.Name,
                Coordinate = coordinate
            };
        
            _tableRepository.Add(table);
        
            return Ok(new { message = "success registering" });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new { message = "registration error" });
        }
    }

    [HttpPatch("update/{tableId}")]
    public IActionResult Patch(int tableId, [FromBody] TableRequest request)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);

            if (table == null)
            {
                return NotFound($"Table with id:{tableId} not found");
            }
           
            if (table.Coordinate == null)
            {
                table.Coordinate = new Coordinate();
            }

            table.Name = request.Name;
            table.Coordinate.Lat = request.Lat;
            table.Coordinate.Lon = request.Lon;

            _tableRepository.Update(table);

            return Ok(new { message = "Successful update" });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(new { message = "Failed to update table" });
        }
    }
    
    [HttpDelete("delete/{tableId}")]
    public ActionResult<string> Delete(int tableId)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);
            
            _tableRepository.Delete(table);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"table with id:{tableId} not exist in DB");
        }
    }
}