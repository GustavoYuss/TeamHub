using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class ProjectProgressModule : PageModel
    {
        private readonly ILogger<ProjectProgressModule> _logger;
        private readonly ITaskManager _TaskManager;

        [BindProperty]
        public List<TaskDTO> TaskList { get; set; }
        public int TotalTasks { get; set; }
        [BindProperty]
        public int PendingPercentage { get; set; }
        [BindProperty]
        public int InProgressPercentage { get; set; }
        [BindProperty]
        public int FinishedPercentage { get; set; }

        public ProjectProgressModule(ILogger<ProjectProgressModule> logger, ITaskManager taskManager)
        {
            _logger = logger;
            _TaskManager = taskManager;
        }

        public void OnGet()
        {
            TaskList = _TaskManager.GetAllTaskByProject(ProjectSinglenton.Id);
            int totalTasks;

            if (TaskList != null)
            {
                totalTasks = TaskList.Count;
                LoadChartData(totalTasks);
            }
            else
            {
                totalTasks = 0;
                TempData["ErrorMessage"] = "Lo siento, hubo un problema con los servidores, " +
                                       "inténtelo más tarde por favor, si el error persiste, " +
                                       "comuníquese con el personal!";
            }
        }

        private void LoadChartData(int totalTasks)
        {
            var pendingTasks = TaskList.Count(t => t.Status == "Actividad Pendiente");
            var inProgressTasks = TaskList.Count(t => t.Status == "Actividad en proceso");
            var finishedTasks = TaskList.Count(t => t.Status == "Actividad Finalizada");
            TotalTasks = totalTasks;
            PendingPercentage = totalTasks > 0 ? (pendingTasks * 100 / totalTasks) : 0;
            InProgressPercentage = totalTasks > 0 ? (inProgressTasks * 100 / totalTasks) : 0;
            FinishedPercentage = totalTasks > 0 ? (finishedTasks * 100 / totalTasks) : 0;
        }


    }
}