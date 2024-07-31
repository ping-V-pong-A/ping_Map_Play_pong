using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;

namespace ping_Map_Play_pong.Service;

public interface ITableService
{
    IEnumerable<TableResponse> GetAll();
    Table GetById(int tableId);
    void PostToDb(TableRequest request);
    void Update(int tableId, TableRequest request);
    void Delete(int tableId);
}