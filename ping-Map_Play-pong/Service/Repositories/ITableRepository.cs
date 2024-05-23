using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ITableRepository
{
    IEnumerable<Table> GetAll();
    Table GetTableByName(int id);
    void AddTable(Table table);
}