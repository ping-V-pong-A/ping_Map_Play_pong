using ping_Map_Play_pong.Model.DataModels;
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

    public IEnumerable<Table> GetAll() => _tableRepository.GetAll();

    public Table GetById(int tableId) => _tableRepository.GetByTableId(tableId);

    public void PostToDb(Table table) => _tableRepository.Add(table);

    public void Update(Table table) => _tableRepository.Update(table);

    public void Delete(int tableId) => _tableRepository.Delete(_tableRepository.GetByTableId(tableId));
}