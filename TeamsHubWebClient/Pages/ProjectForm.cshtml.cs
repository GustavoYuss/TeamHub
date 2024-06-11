using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class ProjectFormModel : PageModel
    {
        private readonly ILogger<ProjectFormModel> _logger;

        private readonly IProjectManager _projectManager;
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        [BindProperty]
        public ProjectDTO projectDTO { get; set; }

        public ProjectFormModel(ILogger<ProjectFormModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
            if (ProjectSinglenton.Id != 0)
            {
                projectDTO = _projectManager.GetProject(ProjectSinglenton.Id);
            }
            else
            {
                projectDTO = new ProjectDTO();
            }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ProjectSinglenton.Id == 0) {
                _projectManager.AddProject(projectDTO, StudentSinglenton.Id);
            } else {
                _projectManager.UpdateProject(projectDTO);
            }

            RedirectToPage("/ActivitiesModule");
        }
    }
}