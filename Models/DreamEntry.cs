namespace _1002_backend.Models;
public class DreamEntry
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateOnly Date { get; set; }
}