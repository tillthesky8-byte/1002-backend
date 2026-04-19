namespace _1002_backend.Dtos.Todos;

public class CreateTodoRequest
{
    public string Text { get; set; } = string.Empty;
    public int? DueAt { get; set; } = null;
    public int TimeFrameId { get; set; }
}

public class UpdateTodoRequest
{
    public string Text { get; set; } = string.Empty;
    public int? DueAt { get; set; }
    public int? FinishedAt { get; set; } = null;
    public int StatusId { get; set; }
    public int TimeFrameId { get; set; }
}

public class TodoResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int CreatedAt { get; set; }
    public int? DueAt { get; set; }
    public int? FinishedAt { get; set; }
    public int StatusId { get; set; }
    public int TimeFrameId { get; set; }
}