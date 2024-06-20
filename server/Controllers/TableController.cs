using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TableController : ControllerBase
{
    private readonly ILogger<TableController> _logger;
    private readonly ITableRepository _tableRepository;
    private readonly ICoordinateRepository _coordinateRepository;
    private readonly ICheckingInRepository _checkingInRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly IPairMatchRepository _pairMatchRepository;

    public TableController(ILogger<TableController> logger, ITableRepository tableRepository, ICoordinateRepository coordinateRepository, IMatchRepository matchRepository, ICheckingInRepository checkingInRepository, IPairMatchRepository pairMatchRepository)
    {
        _logger = logger;
        _tableRepository = tableRepository;
        _coordinateRepository = coordinateRepository;
        _matchRepository = matchRepository;
        _checkingInRepository = checkingInRepository;
        _pairMatchRepository = pairMatchRepository;
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
                Lat = _coordinateRepository.GetById(table.Coordinate.Id).Lat,
                Lon = _coordinateRepository.GetById(table.Coordinate.Id).Lon,
                CheckingIns = _checkingInRepository.GetByTableId(table.Id).ToList(),
                Matches = _matchRepository.GetByTableId(table.Id).ToList(),
                PairMatches = _pairMatchRepository.GetByTableId(table.Id).ToList()
            }).ToList();

            return Ok(respTables);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
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
    public ActionResult<string> Post([FromBody] TableRequest request)
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