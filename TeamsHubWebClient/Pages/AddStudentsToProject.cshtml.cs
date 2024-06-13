using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class AddStudentsToProject : PageModel
    {
         private readonly ILogger<ActivitiesModule> _logger;

        private readonly IUserManager _UserManager;

        private readonly IProjectManager _ProjectManager;

        public int idProject = ProjectSinglenton.Id;

        public string Token = StudentSinglenton.Token;
        
        [BindProperty]
        public List<User>? StudentList {get; set;} 

        public ProjectDTO? ProjectData {get; set;} 

        public List<TaskDTO>? ProjectTask {get; set;}

        public AddStudentsToProject(ILogger<ActivitiesModule> logger, IUserManager userManager, IProjectManager ProjectManager)
        {
            _logger = logger;
            _UserManager = userManager;
            _ProjectManager = ProjectManager;
        }  

        public void OnGet()
        {
            StudentList = _UserManager.GetStudentsByProject(ProjectSinglenton.Id);
            ProjectData = _ProjectManager.GetProject(ProjectSinglenton.Id);
            ProjectTask = _ProjectManager.GetProjectTasksAsync(ProjectSinglenton.Id); 
        }

        public void OnPost() {

        }
    }
}