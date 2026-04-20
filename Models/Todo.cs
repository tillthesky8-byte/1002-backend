namespace _1002_backend.Models;
public class Todo
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public int CreatedAt { get; set; }
    public int? DueAt { get; set; }
    public int? FinishedAt { get; set; }
    public int StatusId { get; set; }
    public int TimeFrameId { get; set; }
}