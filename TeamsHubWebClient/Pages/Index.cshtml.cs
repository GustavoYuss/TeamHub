using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;

namespace TeamsHubWebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProjectManager _projectManager;
        public string Token = StudentSinglenton.Token;
        public List<ProjectDTO> Projects { get; set; }

        [BindProperty]
        public int StudentID { get; set; }

        [BindProperty]
        public ProjectDTO ProjectDTO { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
            StudentID = StudentSinglenton.Id;
        }

        public void OnGet()
        {
            Projects = _projectManager.GetAllMyProjects(StudentSinglenton.Id);
            if(Projects == null)
            {
                TempData["ErrorTitle"] = "Ops... hubo un problema con los servidores";
                TempData["ErrorMessage"] = "Lo siento, hubo un problema con los servidores, " +
                                       "intentelo más tarde por favor, si el error persiste, " +
                                       "comuniquese con el personal!";
            }
        }

        public IActionResult OnPostMove(int IdProject, string NameProject)
        {
            ProjectSinglenton.Id = IdProject;
            ProjectSinglenton.Name = NameProject;
            return RedirectToPage("/ActivitiesModule");
        }

        public async Task<IActionResult> OnPost()
        {
            if (ProjectDTO.StartDate >= ProjectDTO.EndDate)
            {
                SetErrorMessage("La fecha de inicio no puede ser mayor a la fecha de cierre", "Fechas invalidas");
            }
            else
            {
                bool result = ProjectDTO.IdProject == 0
                ? await _projectManager.AddProject(ProjectDTO, StudentSinglenton.Id)
                : await _projectManager.UpdateProject(ProjectDTO);

                if (result)
                {
                    SetSuccessMessage(
                        ProjectDTO.IdProject == 0 ? "Se ha registrado correctamente el proyecto" : "Se ha modificado correctamente el proyecto",
                        ProjectDTO.IdProject == 0 ? "Registro de proyecto exitoso" : "Modificacion de proyecto exitoso"
                    );
                }
            }
            return RedirectToPage("/Index");
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
