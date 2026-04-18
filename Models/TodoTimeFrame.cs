namespace _1002_backend.Models;
public class TodoTimeFrame
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// id = 1 -> "daily"
// id = 2 -> "mid-term"
// id = 3 -> "long-term"