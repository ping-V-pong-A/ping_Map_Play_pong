using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class PairMatchService : IPairMatchService
{
    private readonly ILogger<PairMatchService> _logger;
    private readonly IPairMatchRepository _pairPairMatchRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public PairMatchService(ILogger<PairMatchService> logger, IPairMatchRepository pairPairMatchRepository, IUserRepository userRepository, ITableRepository tableRepository)
    {
        _logger = logger;
        _pairPairMatchRepository = pairPairMatchRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
    }

    public IEnumerable<PairMatch> GetAll()
    {
        return _pairPairMatchRepository.GetAll();
    }

    public PairMatch GetById(int pairMatchId)
    {
        return _pairPairMatchRepository.GetById(pairMatchId);
    }

    public IEnumerable<PairMatch> GetByDate(DateTime date)
    {
        return _pairPairMatchRepository.GetByDate(date);
    }

    public void PostToDb(PairMatchRequest request)
    {
        var team1 = new Team
        {
            Player1 = _userRepository.GetById(request.Team1Player1Id),
            Player2 = _userRepository.GetById(request.Team1Player2Id)
        };
                
        var team2 = new Team
        {
            Player1 = _userRepository.GetById(request.Team2Player2Id),
            Player2 = _userRepository.GetById(request.Team2Player1Id)
        };
            
        var newPairMatch = new PairMatch
        {
            TableId = request.TableId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Team1 = team1,
            Team1Point = request.Team1Point,
            Team2 = team2,
            Team2Point = request.Team2Point
        };
        
        _pairPairMatchRepository.Add(newPairMatch);
    }

    public void Update(int pairMatchId, PairMatchRequest request)
    {
        _logger.LogInformation($"Updating match with ID {pairMatchId}.");
        var pairMatch = _pairPairMatchRepository.GetById(pairMatchId);
            
        var team1 = new Team
        {
            Player1 = _userRepository.GetById(request.Team1Player1Id),
            Player2 = _userRepository.GetById(request.Team1Player2Id)
        };
                
        var team2 = new Team
        {
            Player1 = _userRepository.GetById(request.Team2Player2Id),
            Player2 = _userRepository.GetById(request.Team2Player1Id)
        };
        
        pairMatch.TableId = request.TableId;
        pairMatch.Team1 = team1;
        pairMatch.Team1Point = request.Team1Point;
        pairMatch.Team2 = team2;
        pairMatch.Team1Point = request.Team2Point;
        pairMatch.StartDate = request.StartDate;
        pairMatch.EndDate = request.EndDate;
        
        _pairPairMatchRepository.Update(pairMatch);
    }

    public void DeleteFromDb(int pairMatchId)
    {
        _logger.LogInformation($"Updating match with ID {pairMatchId}.");
        var pairMatch = _pairPairMatchRepository.GetById(pairMatchId);

        if (pairMatch == null)
        {
            throw new NotFoundException($"match with id:{pairMatchId} not exist in DB");
        }
        
        _pairPairMatchRepository.Delete(pairMatch);
    }
}