using _1002_backend.Models;
using _1002_backend.Models.SpecialModels;
namespace _1002_backend.Dtos.Survey;

public class SubmitSurveyRequest
{
    public DateOnly Date { get; set; }
    public List<BuildAnswer> Answers { get; set; } = new List<BuildAnswer>(); //note: create special answer model for request. without id and surveySessionId
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