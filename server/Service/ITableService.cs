using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service;

public interface ITableService
{
    IEnumerable<Table> GetAll();
    Table GetById(int tableId);
    void PostToDb(Table table);
    void Update(Table table);
    void Delete(int tableId);
}