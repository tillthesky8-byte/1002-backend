// DreamEntryPatch.cs

namespace _1002_backend.Models.PatchModels;

public class DreamEntryPatch
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateOnly? Date { get; set; }
}