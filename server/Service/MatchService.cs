using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class MatchService : IMatchService
{
    private readonly ILogger<MatchService> _logger;
    private readonly IMatchRepository _matchRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IServiceMethods _serviceMethods;

    public MatchService(ILogger<MatchService> logger, IMatchRepository matchRepository, IUserRepository userRepository, ITableRepository tableRepository, IServiceMethods serviceMethods)
    {
        _logger = logger;
        _matchRepository = matchRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
        _serviceMethods = serviceMethods;
    }

    public IEnumerable<Match> GetAll()
    {
        _logger.LogInformation("Fetching all matches.");
        return _matchRepository.GetAll();
    }

    public Match GetById(int matchId)
    {
        _logger.LogInformation($"Fetching match with id:{matchId}.");
        var res = _matchRepository.GetById(matchId);

        if (res == null)
        {
            _logger.LogWarning($"Match with ID {matchId} not found.");
            throw new NotFoundException($"match with id:{matchId} not exist in DB");
        }
        return res;
    }

    public IEnumerable<Match> GetByUserId(int userId)
    {
        _logger.LogInformation($"Fetching matches for user with ID {userId}.");
        var res = _matchRepository.GetByUserId(userId).ToList();

        return res;
    }

    public IEnumerable<Match> GetByPlayersId(int player1Id, int player2Id)
    {
        _logger.LogInformation($"Fetching matches for players with IDs {player1Id} and {player2Id}.");
        return _matchRepository.GetByPlayer1IdAndPlayer2Id(player1Id, player2Id);
    }

    public IEnumerable<Match> GetByDate(DateTime date)
    {
        _logger.LogInformation($"Fetching matches for date {date}.");

        return _matchRepository.GetByDate(date);
    }
    public void PostToDb(MatchRequest request)
    {
        _logger.LogInformation("Adding new match to the database.");
        var table = _tableRepository.GetByTableId(request.TableId);
        var player1 = _userRepository.GetById(request.Player1Id);
        var player2 = _userRepository.GetById(request.Player2Id);
            
        var newMatch = new Match
        {
            TableId = table.Id,
            Player1 = player1,
            Player1Point = request.Player1Point,
            Player2 = player2,
            Player2Point = request.Player2Point,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        
        _matchRepository.Add(newMatch);
        _logger.LogInformation("Match successfully added to the database.");

    }

    public void Update(int matchId, MatchRequest request)
    {
        _logger.LogInformation($"Updating match with ID {matchId}.");
        var match = _matchRepository.GetById(matchId);

        if (match == null)
        {
            _logger.LogWarning($"Match with ID {matchId} not found.");
            throw new NotFoundException("404");
        }
        
        var table = _tableRepository.GetByTableId(request.TableId);
        var player1 = _userRepository.GetById(request.Player1Id);
        var player2 = _userRepository.GetById(request.Player2Id);

        match.TableId = table.Id;
        match.Player1 = player1;
        match.Player1Point = request.Player1Point;
        match.Player2 = player2;
        match.Player2Point = request.Player2Point;
        match.StartDate = request.StartDate;
        match.EndDate = request.EndDate;
        
        _serviceMethods.UpdateProperties(request, match);
        
        _matchRepository.Update(match);
        _logger.LogInformation($"Match with ID {matchId} successfully updated.");
    }

    public void DeleteFromDb(int matchId)
    {
        _logger.LogInformation($"Deleting match with ID {matchId} from the database.");
        var match = _matchRepository.GetById(matchId);

        if (match == null)
        {
            _logger.LogWarning($"Match with ID {matchId} not found.");
            throw new NotFoundException("404");
        }
        
        _matchRepository.Delete(match);
        _logger.LogInformation($"Match with ID {matchId} successfully deleted from the database.");
    }

}