using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class TeamService : ITeamService
{
    private readonly ILogger<TeamService> _logger;
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;

    public TeamService(ILogger<TeamService> logger, ITeamRepository teamRepository, IUserRepository userRepository)
    {
        _logger = logger;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<Team> GetAll()
    {
        return _teamRepository.GetAll();
    }

    public IEnumerable<Team> GetByUserId(int userId)
    {
        return _teamRepository.GetByUserId(userId);
    }

    public Team GetByPlayersId(int player1Id, int player2Id)
    {
        return _teamRepository.GetByPlayersId(player1Id, player2Id);
    }

    public void PostToDb(int player1Id, int player2Id)
    {
        if (_teamRepository.GetByPlayersId(player1Id, player2Id) != null)
        {
            _logger.LogInformation("This team already exist");
            throw new BadRequestException("This team already exist");
        }
        
        var player1 = _userRepository.GetById(player1Id);
        var player2 = _userRepository.GetById(player2Id);
                
        var newTeam = new Team
        {
            Player1 = player1,
            Player2 = player2
        };
        
        _teamRepository.Add(newTeam);
    }

    public void DeleteFromDb(int teamId)
    {
        var team = _teamRepository.GetById(teamId);
        
        if (team != null)
        {
            throw new NotFoundException("");
        }
        
        _teamRepository.Delete(team);
    }
}