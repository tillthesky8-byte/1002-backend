namespace _1002_backend.Models;
public class QuestionType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

// id = 1 -> "text"
// id = 2 -> "integer"
// id = 3 -> "boolean"
// id = 4 -> "scale"
// id = 5 -> "time"

