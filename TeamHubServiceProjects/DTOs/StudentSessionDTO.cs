
namespace TeamHubServiceProjects.DTOs
{
    public class StudentSessionDTO
    {
        public StudentDTO Student {get; set;}    
        public string Token {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime LastAccess {get; set;}
    }
}