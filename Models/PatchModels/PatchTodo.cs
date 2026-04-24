// Patch.cs
namespace _1002_backend.Models.PatchModels;
public class TodoPatch
{
    public string? Text { get; set; }
    public int? DueAt { get; set; }
    public int? FinishedAt { get; set; }
    public int? StatusId { get; set; }
    public int? TimeFrameId { get; set; }
}