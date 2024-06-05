using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ITableRepository
{
    IEnumerable<Table> GetAll();
    Table GetByTableName(string tableName);
    Table GetByTableId(int tableId);
    void Add(Table table);
    void Delete(Table table);
    void Update(Table table);
}