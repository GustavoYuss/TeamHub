
namespace TeamHubServiceProjects.DTOs
{
    public class StudentValidationResponse
    {
        public bool IsValid {get; set;}
        public string Token {get; set;}
        public StudentDTO? Student {get; set;}
    }
}