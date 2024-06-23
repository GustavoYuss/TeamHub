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
                TempData["ErrorTitle"] = "Ops... hubo un problema con los servidores";
                TempData["ErrorMessage"] = "Lo siento, hubo un problema con los servidores, " +
                                       "intentelo más tarde por favor, si el error persiste, " +
                                       "comuniquese con el personal!";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Task.StartDate >= Task.EndDate)
            {
                SetErrorMessage("La fecha de inicio no puede ser mayor a la fecha de cierre", "Fechas invalidas");
            }
            else
            {
                Task.IdProject = ProjectSinglenton.Id;
                bool result = Task.IdTask == 0
                ? await _TaskManager.AddTask(Task)
                : await _TaskManager.UpdateTask(Task);
            }
            return RedirectToPage("/ActivitiesModule");
        }

        private void SetSuccessMessage(string message, string title)
        {
            TempData["Message"] = message;
            TempData["Title"] = title;
        }

        private void SetErrorMessage(string message, string title)
        {
            TempData["ErrorTitle"] = title;
            TempData["ErrorMessage"] = message;
        }
    } 
}