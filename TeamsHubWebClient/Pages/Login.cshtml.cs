using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserIdentityManager _UserIdentityManager;

        public readonly IProjectManager _ProjectManager;

        [BindProperty]
        public SessionLoginRequest sessionLoginRequest {get; set;}

        public LoginModel(ILogger<LoginModel> logger, IUserIdentityManager userIdentityManager)
        {
            _logger = logger;
            _UserIdentityManager = userIdentityManager;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (sessionLoginRequest != null)
            {
                _logger.LogInformation(sessionLoginRequest.Password);
                var response = _UserIdentityManager.ValidateUser(sessionLoginRequest);
                if (response.IsValid)
                {
                    HttpContext.Session.SetString("token_usuario", response.token);
                    StudentSinglenton.Id = response.User.Id;
                    StudentSinglenton.Email = response.User.Email;
                    StudentSinglenton.FullName = response.User.FullName;
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }
    }
}