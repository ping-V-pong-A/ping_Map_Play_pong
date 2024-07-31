using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class TableService : ITableService
{
    private readonly ILogger<TableService> _logger;
    private readonly ITableRepository _tableRepository;

    public TableService(ILogger<TableService> logger, ITableRepository tableRepository)
    {
        _logger = logger;
        _tableRepository = tableRepository;
    }

    public IEnumerable<TableResponse> GetAll()
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
        
        return respTables;
    }

    public Table GetById(int tableId)
    {
        return _tableRepository.GetById(tableId);
    }

    public void PostToDb(TableRequest request)
    {
        var coordinate = new Coordinate
        {
            Lat = request.Lat,
            Lon = request.Lon
        };
        
        var newTable = new Table
        {
            Name = request.Name,
            Coordinate = coordinate
        };
        
        _tableRepository.Add(newTable);
    }

    public void Update(int tableId, TableRequest request)
    {
        var table = _tableRepository.GetById(tableId);
        
        if (table == null)
        {
            throw new NotFoundException($"table with id:{tableId} not exist in DB");
        }

        table.Name = request.Name;
        table.Coordinate.Lat = request.Lat;
        table.Coordinate.Lon = request.Lon;
        
        _tableRepository.Update(table);
    }

    public void Delete(int tableId)
    {
        var table = _tableRepository.GetById(tableId);
        
        if (table == null)
        {
            throw new NotFoundException($"table with id:{tableId} not exist in DB");
        }
        
        _tableRepository.Delete(table);
    }
}