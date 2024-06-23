using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserIdentityManager _UserIdentityManager;

        [BindProperty]
        public SessionLoginRequest sessionLoginRequest {get; set;}

        public LoginModel(ILogger<LoginModel> logger, IUserIdentityManager userIdentityManager)
        {
            _logger = logger;
            _UserIdentityManager = userIdentityManager;
        }

        public void OnGet() {}

        public IActionResult OnPost()
        {

            if (VerifyFields())
            {
                _logger.LogInformation(sessionLoginRequest.Password);
                var response = _UserIdentityManager.ValidateUser(sessionLoginRequest);
                string successMessage;
                string successTitle;

                if (response != null)
                {
                    if (response.IsValid)
                    {
                        HttpContext.Session.SetString("token_usuario", response.token);
                        StudentSinglenton.Id = response.User.Id;
                        StudentSinglenton.Email = response.User.Email;
                        StudentSinglenton.FullName = response.User.FullName;
                        StudentSinglenton.Token = response.token;
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        successTitle = "Credenciales incorrectas";
                        successMessage = "El correo y al contrasena no coinciden," +
                            " verifique sus campos de nuevo por favor!";
                    }
                }
                else
                {
                    successTitle = "Oops... hubo error con los servidores";
                    successMessage = "Lo siento, hubo un problema con los servidores, " +
                                       "intentelo mas tarde por favor, si el error persiste, " +
                                       "comuniquese con el personal!";
                }

                SetMessage(successMessage, successTitle);
            }

            return Page();
        }

        private bool VerifyFields()
        {
            bool result = true;
            string successMessage;
            string successTitle;

            if (string.IsNullOrEmpty(sessionLoginRequest.Email) || string.IsNullOrEmpty(sessionLoginRequest.Password))
            {
                result = false;
                successTitle = "Campos invalidos";
                successMessage = "El correo ni la contrasena deben ser espacios en blanco, ni nulos";
                SetMessage(successMessage, successTitle);
            }
            else
            {
                if (sessionLoginRequest.Password.Length < 8)
                {
                    result = false;
                    successTitle = "contraseña invalida";
                    successMessage = "La contrasena debe tener al menos 8 caracteres";
                    SetMessage(successMessage, successTitle);
                }
            }

            return result;
        }

        private void SetMessage(string message, string title)
        {
            TempData["Message"] = message;
            TempData["Title"] = title;
        }
    }
}