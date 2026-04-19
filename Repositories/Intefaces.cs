using _1002_backend.Models;

namespace _1002_backend.Repositories.Interfaces;

public interface IDreamEntryRepository
{
    Task<IEnumerable<DreamEntry>> GetAllDreamEntries(int pageNumber, int pageSize);
    Task<DreamEntry?> GetDreamEntryById(int id);
    Task<bool> CreateDreamEntry(DreamEntry entry);
    Task<bool> UpdateDreamEntry(DreamEntry entry);
    Task<bool> DeleteDreamEntry(int id);

    Task<IEnumerable<DreamEntry>> GetDreamEntriesByDate(DateOnly date);
}

public interface ISurveyRepository
{
    Task<bool> SurveyExistsToday();
    Task<bool> SurveySubmit(SurveySession session, List<Answer> answers);
}


public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetAllTodos(int pageNumber, int pageSize);
    Task<Todo?> GetTodoById(int id);
    Task<bool> CreateTodo(Todo todo);
    Task<bool> UpdateTodo(Todo todo);
    Task<bool> DeleteTodo(int id);
}