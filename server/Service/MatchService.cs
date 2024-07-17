using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IMatchRepository _matchRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public MatchService(ILogger<MatchService> logger, IMatchRepository matchRepository, IUserRepository userRepository, ITableRepository tableRepository)
    {
        _logger = logger;
        _matchRepository = matchRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
    }

    public IEnumerable<Match> GetAll() => _matchRepository.GetAll();

    public Match GetById(int matchId) => _matchRepository.GetById(matchId);

    public IEnumerable<Match> GetByUserId(int userId) => _matchRepository.GetByUserId(userId);

    public IEnumerable<Match> GetByPlayersId(int player1Id, int player2Id) =>
        _matchRepository.GetByPlayer1IdAndPlayer2Id(player1Id, player2Id);

    public IEnumerable<Match> GetByDate(DateTime date) => _matchRepository.GetByDate(date);

    public void PostToDb(MatchRequest request)
    {
        var table = _tableRepository.GetByTableId(request.TableId);
        var player1 = _userRepository.GetById(request.Player1Id);
        var player2 = _userRepository.GetById(request.Player2Id);
            
        var newMatch = new Match
        {
            TableId = table.Id,
            Player1 = player1,
            Player2 = player2,
            StartDate = request.StartTime,
            EndDate = request.EndTime
        };
        
        _matchRepository.Add(newMatch);
    }

    public void Update(Match match) => _matchRepository.Update(match);

    public void DeleteFromDb(Match match) => _matchRepository.Delete(match);
}