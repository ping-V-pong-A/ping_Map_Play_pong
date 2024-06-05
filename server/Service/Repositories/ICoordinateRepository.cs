using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ICoordinateRepository
{
    IEnumerable<Coordinate> GetAll();
    Coordinate GetById(int coordinateId);
    void Add(Coordinate coordinate);
    void Delete(Coordinate coordinate);
    void Update(Coordinate coordinate);
}