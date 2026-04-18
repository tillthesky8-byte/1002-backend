namespace _1002_backend.Models;
public class Answer
{
    public int Id { get; set; }
    public int SurveySessionId { get; set; }
    public int QuestionId { get; set; }
    public string? Response { get; set; }
    public string? Remark { get; set; }
}