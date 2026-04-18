namespace _1002_backend.Dtos.DreamEntries;

public class CreateDreamEntryRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}

public class UpdateDreamEntryRequest
{
    public string? Title { get; set; } = null;
    public string? Content { get; set; } = null;
    public DateOnly Date { get; set; }
}

public class DreamEntryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
}