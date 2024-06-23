
using System.ComponentModel.DataAnnotations;

namespace TeamsHubWebClient.DTOs;

public partial class SessionLoginRequest
{
    public String Email {get; set;}
    public String Password {get; set;}
    public String RemindMe {get; set;}
}