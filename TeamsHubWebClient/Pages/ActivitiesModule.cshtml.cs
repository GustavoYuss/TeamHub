using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class ActivitiesModule : PageModel
    {
        private readonly ILogger<ActivitiesModule> _logger;

        private readonly ITaskManager _TaskManager;

        [BindProperty]
        public TaskDTO Task { get; set; }
        public List<TaskDTO> TaskList { get; set; }

        public ActivitiesModule(ILogger<ActivitiesModule> logger, ITaskManager taskManager)
        {
            _logger = logger;
            _TaskManager = taskManager;
            Task = new TaskDTO();
            Task.IdTask = 0;
        }

        public void OnGet()
        {
            TaskList = _TaskManager.GetAllTaskByProject(ProjectSinglenton.Id);
            if(TaskList == null)
            {
                TempData["ErrorMessage"] = "Lo siento, hubo un problema con los servidores, " +
                                       "inténtelo más tarde por favor, si el error persiste, " +
                                       "comuníquese con el personal!";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool result;
            Task.IdProject = ProjectSinglenton.Id;
            if (Task.IdTask == 0)
            {
                result = await _TaskManager.AddTask(Task);
                Console.WriteLine("AddTask executed, result: " + result);

            }
            else
            {
                result = await _TaskManager.UpdateTask(Task);
                Console.WriteLine("UpdateTask executed, result: " + result);
            }
            return RedirectToPage("/ActivitiesModule");
        }
    }
}