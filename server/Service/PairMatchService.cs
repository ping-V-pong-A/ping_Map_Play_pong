using ping_Map_Play_pong.Model.DataModels;
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

    public IEnumerable<PairMatch> GetAll() => _pairPairMatchRepository.GetAll();

    public PairMatch GetById(int pairMatchId) => _pairPairMatchRepository.GetById(pairMatchId);

    public IEnumerable<PairMatch> GetByDate(DateTime date) => _pairPairMatchRepository.GetByDate(date);

    public void PostToDb(PairMatchRequest request)
    {
        var table = _tableRepository.GetByTableId(request.TableId);
            
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
            TableId = table.Id,
            Team1 = team1,
            Team2 = team2,
            StartDate = request.StartTime,
            EndDate = request.EndTime
        };
        
        _pairPairMatchRepository.Add(newPairMatch);
    }

    public void Update(PairMatch pairMatch) => _pairPairMatchRepository.Update(pairMatch);

    public void DeleteFromDb(PairMatch pairMatch) => _pairPairMatchRepository.Delete(pairMatch);
}