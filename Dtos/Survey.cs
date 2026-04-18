using _1002_backend.Models;

namespace _1002_backend.Dtos.Survey;

public class SubmitSurveyRequest
{
    public DateOnly Date { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
}

public class SurveyResponse
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }

}

public class SurveyExistenceResponse
{
    public bool Exists { get; set; }
}