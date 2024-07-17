using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class CheckingInService : ICheckingInService
{
    private readonly ILogger<CheckingInService> _logger;
    private readonly ICheckingInRepository _checkingInRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public CheckingInService(ILogger<CheckingInService> logger, ICheckingInRepository checkingInRepository, IUserRepository userRepository, ITableRepository tableRepository)
    {
        _logger = logger;
        _checkingInRepository = checkingInRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
    }

    public IEnumerable<CheckingIn> GetAll() => _checkingInRepository.GetAll();
    public CheckingIn GetById(int checkingInId) => _checkingInRepository.GetById(checkingInId);
    public IEnumerable<CheckingIn> GetByUserId(int userId) => _checkingInRepository.GetByUserId(userId);
    public void PostToDb(CheckInRequest request)
    {
        var user = _userRepository.GetById(request.UserId);
        var table = _tableRepository.GetByTableId(request.TableId);
            
        var newCheckingIn = new CheckingIn
        {
            UserId = user.Id,
            TableId = table.Id,
            StartDate = request.Start,
            EndDate = request.End
        };
        _checkingInRepository.Add(newCheckingIn);
    }
    public void Update(CheckingIn checkingIn) => _checkingInRepository.Update(checkingIn);
    public void DeleteFromDb(CheckingIn checkingIn) => _checkingInRepository.Delete(checkingIn);
}