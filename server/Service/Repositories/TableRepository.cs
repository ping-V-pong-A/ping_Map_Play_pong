using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class TableRepository : ITableRepository
{
    private PingMapPlayPongContext _dbContext;

    public TableRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<Table> GetAll()
    {
        return _dbContext.Tables.ToList();
    }


    public Table GetByTableName(string tableName)
    {
        return _dbContext.Tables.FirstOrDefault(t => t.Name == tableName);
    }


    public Table GetByTableId(int tableId)
    {
        return _dbContext.Tables.FirstOrDefault(t => t.Id == tableId);
    }


    public void Add(Table table)
    {
        _dbContext.Add(table);
        _dbContext.SaveChanges();
    }

    public void Delete(Table table)
    {
        _dbContext.Remove(table);
        _dbContext.SaveChanges();
    }

    public void Update(Table table)
    {
        _dbContext.Update(table);
        _dbContext.SaveChanges();
    }
    
}