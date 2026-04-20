using _1002_backend.Models;
using _1002_backend.Repositories.Interfaces;
using _1002_backend.Services.Interfaces;


namespace _1002_backend.Services;
public class DreamEntryService : IDreamEntryService
{
    private readonly IDreamEntryRepository _dreamEntryRepository;
    private readonly ILogger<DreamEntryService> _logger;

    public DreamEntryService(IDreamEntryRepository dreamEntryRepository, ILogger<DreamEntryService> logger)
    {
        _dreamEntryRepository = dreamEntryRepository;
        _logger = logger;
    }

    public Task<IEnumerable<DreamEntry>> GetAllDreamEntries(int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1; _logger.LogWarning("\n ---START--- \n \n Page number less than 1, defaulting to 1 \n \n ---END--- \n");
        if (pageSize < 1) pageSize = 10; _logger.LogWarning("\n ---START--- \n \n Page size less than 1, defaulting to 10 \n \n ---END--- \n");
        //note: remember to remove such validation at the repository layer.
        try {
            _logger.LogInformation("\n ---START--- \n \n Delegating GetAllDreamEntries to repository with \n pageNumber: {PageNumber}, pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
            return _dreamEntryRepository.GetAllDreamEntries(pageNumber, pageSize);
        }     
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in GetAllDreamEntries with \n pageNumber: {PageNumber}, pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
            throw;
        }
    }

    public async Task<DreamEntry?> GetDreamEntryById(int id)
    {
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating GetDreamEntryById to repository with id: {Id} \n \n ---END--- \n", id);
            return await _dreamEntryRepository.GetDreamEntryById(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in GetDreamEntryById with id: {Id} \n \n ---END--- \n", id);
            throw;
        }
    }   

    public async Task<bool> CreateDreamEntry(string title, string content, DateOnly? date)
    {
        try {
            _logger.LogInformation("\n ---START--- \n \n Creating DreamEntry entity with title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", title, content, date);  
            var entry = new DreamEntry
            {
                Title = title,
                Description = content, // note: mapping Content to Description, rename in the future.
                Date = date ?? DateOnly.FromDateTime(DateTime.UtcNow)
            };

            _logger.LogInformation("\n ---START--- \n \n Entity is created -> Delegating CreateDreamEntry to repository with title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", title, content, date);

            return await _dreamEntryRepository.CreateDreamEntry(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in CreateDreamEntry with title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", title, content, date);
            throw;
        }
    }

    public async Task<bool> UpdateDreamEntry(string title, string content, DateOnly date, int id)
    {  
        try {
            _logger.LogInformation("\n ---START--- \n \n Creating DreamEntry entity for update with id: {Id}, title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", id, title, content, date);
            var entry = new DreamEntry
            {
                Id = id,
                Title = title,
                Description = content,
                Date = date
            };
            _logger.LogInformation("\n ---START--- \n \n Entity is created -> Delegating UpdateDreamEntry to repository with id: {Id}, title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", id, title, content, date);
            return await _dreamEntryRepository.UpdateDreamEntry(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in UpdateDreamEntry with id: {Id}, title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", id, title, content, date);
            throw;
        }
    }

    public async Task<bool> DeleteDreamEntry(int id)
    {   
        try 
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating DeleteDreamEntry to repository with id: {Id} \n \n ---END--- \n", id);
            return await _dreamEntryRepository.DeleteDreamEntry(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in DeleteDreamEntry with id: {Id} \n \n ---END--- \n", id);
            throw;
        }

    }
    public async Task<IEnumerable<DreamEntry>> GetDreamEntriesByDate(DateOnly date)
    {
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating GetDreamEntriesByDate to repository with date: {Date} \n \n ---END--- \n", date);
            return await _dreamEntryRepository.GetDreamEntriesByDate(date);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in GetDreamEntriesByDate with date: {Date} \n \n ---END--- \n", date);
            throw;
        }
    }
}