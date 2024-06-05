using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class CoordinateRepository : ICoordinateRepository
{
    private pingMapPlayPongContext _dbContext;

    public CoordinateRepository(pingMapPlayPongContext context)
    {
        _dbContext = context;
    }


    public IEnumerable<Coordinate> GetAll()
    {
        return _dbContext.Coordinates.ToList();
    }


    public Coordinate GetById(int coordinateId)
    {
        return _dbContext.Coordinates.FirstOrDefault(c => c.Id == coordinateId);
    }


    public void Add(Coordinate coordinate)
    {
        _dbContext.Add(coordinate);
        _dbContext.SaveChanges();
    }


    public void Delete(Coordinate coordinate)
    {
        _dbContext.Remove(coordinate);
        _dbContext.SaveChanges();
    }

    public void Update(Coordinate coordinate)
    {
        _dbContext.Update(coordinate);
        _dbContext.SaveChanges();
    }
}