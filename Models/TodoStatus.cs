namespace _1002_backend.Models;
public class TodoStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// id = 1 -> "ongoing"
// id = 2 -> "completed"
// id = 3 -> "failed"