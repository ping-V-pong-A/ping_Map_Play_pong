using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CoordinateController : ControllerBase
{
    private readonly ILogger<CoordinateController> _logger;
    private readonly ICoordinateRepository _coordinateRepository;

    public CoordinateController(ILogger<CoordinateController> logger, ICoordinateRepository coordinateRepository)
    {
        _logger = logger;
        _coordinateRepository = coordinateRepository;
    }

    [HttpGet(Name = "coordinates")]
    public ActionResult<IEnumerable<Coordinate>> GetAll()
    {
        try
        {
            return Ok(_coordinateRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("coordinates table is empty");
        }
    }

    [HttpGet("coordinates/id/{coordinateId}")]
    public ActionResult<Coordinate> GetById(int coordinateId)
    {
        try
        {
            return Ok(_coordinateRepository.GetById(coordinateId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"coordinate with id:{coordinateId} not exist in DB");
        }
    }
    
    [HttpPost("coordinates/add/lat={lat}&lon={lon}")]
    public ActionResult<string> Post(int lat, int lon)
    {
        try
        {
            var newCoordinate = new Coordinate
            {
                Lat = lat,
                Lon = lon
            };
            _coordinateRepository.Add(newCoordinate);
            
            return Ok("success added new coordinate");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new coordinate");
        }
    }

    [HttpPatch("coordinates/id/{coordinateId}")]
    public ActionResult<string> Update(int coordinateId)
    {
        try
        {
            var coordinate = _coordinateRepository.GetById(coordinateId);
            
            _coordinateRepository.Update(coordinate);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"coordinate with id:{coordinateId} not exist in DB");
        }
    }

    [HttpDelete("coordinates/id/{coordinateId}")]
    public ActionResult<string> Delete(int coordinateId)
    {
        try
        {
            var coordinate = _coordinateRepository.GetById(coordinateId);
            
            _coordinateRepository.Delete(coordinate);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"coordinate with id:{coordinateId} not exist in DB");
        }
    }
}