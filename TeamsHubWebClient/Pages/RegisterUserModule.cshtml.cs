using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class RegisterUserModel : PageModel
    {
        private readonly ILogger<RegisterUserModel> _logger;
        private readonly IUserManager _userManager;
        [BindProperty]
        public StudentDTO studentDTO { get; set; }

        public RegisterUserModel(ILogger<RegisterUserModel> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
            studentDTO = new StudentDTO();
        }

        public void OnGet() { }

        public void OnPost()
        {
            var messageErrors = AreFieldsValid(studentDTO);

            if (messageErrors.Count > 0)
            {
                foreach (var error in messageErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            if (messageErrors.Count == 0)
            {
                _userManager.AddStudent(studentDTO);
                RedirectToPage("/Index");
            }
        }


        private List<string> AreFieldsValid(StudentDTO studentDTO)
        {
            bool isValid = true;
            List<string> messeageErrors = new List<string>();

            if (string.IsNullOrEmpty(studentDTO.Name))
            {
                messeageErrors.Add("El nombre es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(studentDTO.Email))
            {
                messeageErrors.Add("El correo es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(studentDTO.SurName))
            {
                messeageErrors.Add("El apellido materno es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(studentDTO.LastName))
            {
                messeageErrors.Add("El apellido paterno es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(studentDTO.MiddleName))
            {
                messeageErrors.Add("El apodo es obligatorio.");
                isValid = false;
            }

            if (string.IsNullOrEmpty(studentDTO.Password))
            {
                messeageErrors.Add("El contraseña es obligatorio.");
                isValid = false;
            }

            return messeageErrors;
        }
    }
}