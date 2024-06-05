using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TableController : ControllerBase
{
    private readonly ILogger<TableController> _logger;
    private readonly ITableRepository _tableRepository;
    private readonly ICoordinateRepository _coordinateRepository;

    public TableController(ILogger<TableController> logger, ITableRepository tableRepository, ICoordinateRepository coordinateRepository)
    {
        _logger = logger;
        _tableRepository = tableRepository;
        _coordinateRepository = coordinateRepository;
    }

    [HttpGet(Name = "tables")]
    public ActionResult<IEnumerable<Table>> GetAll()
    {
        try
        {
            return Ok(_tableRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("tables table is empty");
        }
    }

    [HttpGet("tables/id/{tableId}")]
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
    
    [HttpGet("tables/name/{tableName}")]
    public ActionResult<Table> GetByName(string tableName)
    {
        try
        {
            return Ok(_tableRepository.GetByTableName(tableName));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"table with id:{tableName} not exist in DB");
        }
    }
    
    [HttpPost("tables/add")]
    public ActionResult<string> Post(string tableName, double lat, double lon)
    {
        try
        {
            var coordinate = new Coordinate
            {
                Lat = lat,
                Lon = lon
            };
            
            var table = new Table
            {
                Name = tableName,
                Coordinate = coordinate
            };
            
            _tableRepository.Add(table);
            
            return Ok("success registering");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success registering");
        }
    }

    [HttpPatch("tables/id/{tableId}")]
    public ActionResult<string> Update(int tableId)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);
            
            _tableRepository.Update(table);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"table with id:{tableId} not exist in DB");
        }
    }
    
    [HttpDelete("tables/id/{tableId}")]
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