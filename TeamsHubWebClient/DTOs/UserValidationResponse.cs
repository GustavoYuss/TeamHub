
namespace TeamsHubWebClient.DTOs;

public partial class UserValidationResponse
{
    public bool IsValid {get; set;}
    public User? User {get; set;}
    public string token {get; set;}
}