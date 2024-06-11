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
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (StudentSinglenton.Id  == 0) {
                _userManager.AddStudent(studentDTO);
            } else {
                _userManager.EditStudent(studentDTO);
            }

            return Page();
        }
    }
}