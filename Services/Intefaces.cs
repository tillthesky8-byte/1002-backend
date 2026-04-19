using _1002_backend.Models;
namespace _1002_backend.Services.Interfaces;

public interface IDreamEntryService
{
    Task<IEnumerable<DreamEntry>> GetAllDreamEntries(int pageNumber, int pageSize);
    Task<DreamEntry?> GetDreamEntryById(int id);
    Task<bool> CreateDreamEntry(string title, string content, DateOnly? date);
    Task<bool> UpdateDreamEntry(string title, string content, DateOnly date, int id);
    Task<bool> DeleteDreamEntry(int id);
    Task<IEnumerable<DreamEntry>> GetDreamEntriesByDate(DateOnly date);
}

public interface ISurveyService
{
    Task<bool> SurveyExistsToday();
    Task<bool> SubmitSurvey(DateOnly date, List<Answer> answers);
}

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllTodos(int pageNumber, int pageSize);
    Task<Todo?> GetTodoById(int id);
    Task<bool> CreateTodo(string text, int dueAt, int statusId, int timeFrameId);
    Task<bool> UpdateTodo(string text, int dueAt, int finishedAt, int statusId, int timeFrameId, int id);
    Task<bool> DeleteTodo(int id);

    Task<bool> MarkTodoAsFinished(int id, bool isFailed);
}