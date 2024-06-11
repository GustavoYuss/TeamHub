
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IProjectManager _projectManager;
        
        [BindProperty]
        public DateTime StartDate {get; set;}
        [BindProperty]
        public DateTime EndDate {get; set;}
        public List<ProjectDTO> listaCursos {get; set;}       

        public IndexModel(ILogger<IndexModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
            ProjectSinglenton.Id = 0;
        }  
        
        public void OnGet()
        {
            listaCursos = _projectManager.GetAllMyProjects(StudentSinglenton.Id);            
        }

        public void OnPostFechas() {                        
            //listaCursos = _projectManager.GetProjectsbyDate(StartDate, EndDate);        
        }

        public IActionResult OnPostMove(int IdProject, string NameProject)
        {
            ProjectSinglenton.Id = IdProject;
            ProjectSinglenton.Name = NameProject;
            return RedirectToPage("/ActivitiesModule");
        }
    }
}