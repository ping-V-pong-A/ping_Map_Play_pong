using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class CheckingInService : ICheckingInService
{
    private readonly ILogger<CheckingInService> _logger;
    private readonly IServiceMethods _serviceMethods;
    private readonly ICheckingInRepository _checkingInRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public CheckingInService(ILogger<CheckingInService> logger, ICheckingInRepository checkingInRepository, IUserRepository userRepository, ITableRepository tableRepository, IServiceMethods serviceMethods)
    {
        _logger = logger;
        _checkingInRepository = checkingInRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
        _serviceMethods = serviceMethods;
    }

    public IEnumerable<CheckingIn> GetAll()
    {
        _logger.LogInformation("Fetching all CheckingIns");
        var result = _checkingInRepository.GetAll();
        _logger.LogInformation($"Retrieved {result.Count()} CheckingIn");

        return result;
    }

    public CheckingIn GetById(int checkingInId)
    {
        _logger.LogInformation($"Fetching CheckingIn with Id: {checkingInId}.");
        var checkingIn = _checkingInRepository.GetById(checkingInId);
        if (checkingIn == null)
        {
            _logger.LogInformation($"CheckingIn with Id:{checkingInId} not exist in the DB");
            throw new NotFoundException($"CheckingIn with Id:{checkingInId} not exist in the DB");
        }
        
        _logger.LogInformation($"Retrieved CheckingIn with Id: {checkingInId}.");
        return _checkingInRepository.GetById(checkingInId);
    }

    public IEnumerable<CheckingIn> GetByUserId(int userId)
    {
        _logger.LogInformation($"Fetching CheckingIn for UserId: {userId}.");
        var result = _checkingInRepository.GetByUserId(userId);
        _logger.LogInformation($"Retrieved {result.Count()} CheckingIn for UserId: {userId}.");

        return result;
    }
    
    public void PostToDb(CheckInRequest request)
    {
       
        var startDateUtc = request.StartDate.ToUniversalTime();
        var endDateUtc = request.EndDate.ToUniversalTime();

        _logger.LogInformation($"Adding new CheckingIn. UserId: {request.UserId}, TableId: {request.TableId}, StartDate: {startDateUtc}, EndDate: {endDateUtc}.");

        var user = _userRepository.GetById(request.UserId);
        var table = _tableRepository.GetById(request.TableId);
    
        if (user == null || table == null)
        {
            _logger.LogError($"UserId: {request.UserId} or TableId: {request.TableId} does not exist.");
            throw new NotFoundException($"UserId: {request.UserId} or TableId: {request.TableId} does not exist.");
        }

        var newCheckingIn = new CheckingIn
        {
            UserId = user.Id,
            TableId = table.Id,
            StartDate = startDateUtc,
            EndDate = endDateUtc
        };
    
        _checkingInRepository.Add(newCheckingIn);
        _logger.LogInformation($"New CheckingIn added for UserId: {request.UserId}, TableId: {request.TableId}.");
    }

    
    public void Update(int checkingInId, CheckInRequest request)
    {
        var checkingIn = _checkingInRepository.GetById(checkingInId);
        
        if (checkingIn == null)
        {
            _logger.LogInformation($"CheckingIn with Id:{checkingInId} not exist in the DB");
            throw new NotFoundException($"CheckingIn with Id:{checkingInId} not exist in the DB");
        }
        
        _serviceMethods.UpdateProperties(request, checkingIn);
        
        _checkingInRepository.Update(checkingIn);
    }

    public void DeleteFromDb(int checkingInId)
    {
        var checkingIn = _checkingInRepository.GetById(checkingInId);
        
         if (checkingIn == null)
         {
             _logger.LogInformation($"CheckingIn with Id:{checkingInId} not exist in the DB");
             throw new NotFoundException($"CheckingIn with Id:{checkingInId} not exist in the DB");
         }

         _checkingInRepository.Delete(checkingIn);
    }
}