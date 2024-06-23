using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostAsync()
        {
            var messageErrors = AreFieldsValid(studentDTO);

            if (messageErrors.Count > 0)
            {
                foreach (var error in messageErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return Page();
            }

            if (ModelState.IsValid)
            {
                _userManager.AddStudent(studentDTO);
                return RedirectToPage("/Index");
            }

            return Page();
        }

        private List<string> AreFieldsValid(StudentDTO studentDTO)
        {
            var messageErrors = new List<string>();

            if (string.IsNullOrEmpty(studentDTO.Name))
            {
                messageErrors.Add("El nombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(studentDTO.Email))
            {
                messageErrors.Add("El correo es obligatorio.");
            }

            if (string.IsNullOrEmpty(studentDTO.SurName))
            {
                messageErrors.Add("El apellido materno es obligatorio.");
            }

            if (string.IsNullOrEmpty(studentDTO.LastName))
            {
                messageErrors.Add("El apellido paterno es obligatorio.");
            }

            if (string.IsNullOrEmpty(studentDTO.MiddleName))
            {
                messageErrors.Add("El apodo es obligatorio.");
            }

            if (string.IsNullOrEmpty(studentDTO.Password))
            {
                messageErrors.Add("El contraseña es obligatorio.");
            }

            return messageErrors;
        }
    }
}
