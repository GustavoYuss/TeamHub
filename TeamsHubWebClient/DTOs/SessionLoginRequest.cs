
using System.ComponentModel.DataAnnotations;

namespace TeamsHubWebClient.DTOs;

public partial class SessionLoginRequest
{
    [Required(ErrorMessage = "El email es requerido.")]
    [EmailAddress(ErrorMessage = "El email no es válido.")]
    public String Email {get; set;}
    
    [Required(ErrorMessage = "La contraseña es requerida.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    public String Password {get; set;}
    public String RemindMe {get; set;}
}