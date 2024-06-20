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
        public List<ProjectDTO> listaCursos { get; set; }

        [BindProperty]
        public ProjectDTO ProjectDTO { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProjectManager projectManager)
        {
            _logger = logger;
            _projectManager = projectManager;
        }

        public void OnGet()
        {
            listaCursos = _projectManager.GetAllMyProjects(StudentSinglenton.Id);
        }

        public IActionResult OnPostMove(int IdProject, string NameProject)
        {
            ProjectSinglenton.Id = IdProject;
            ProjectSinglenton.Name = NameProject;
            return RedirectToPage("/ActivitiesModule");
        }

        public async Task<IActionResult> OnPost()
        {
            bool result;
            string successMessage;
            string successTitle;

            if (ProjectDTO.IdProject == 0)
            {
                result = await _projectManager.AddProject(ProjectDTO, StudentSinglenton.Id);
                successMessage = "Se ha registrado correctamente el proyecto";
                successTitle = "Registro de proyecto exitoso";
            }
            else
            {
                result = await _projectManager.UpdateProject(ProjectDTO);
                successMessage = "Se ha modificado correctamente el proyecto";
                successTitle = "Modificación de proyecto exitoso";
            }

            if (result)
            {
                SetSuccessMessage(successMessage, successTitle);
            }
            else
            {
                SetErrorMessage();
            }

            return RedirectToPage("/Index");
        }

        private void SetSuccessMessage(string message, string title)
        {
            TempData["Message"] = message;
            TempData["Title"] = title;
        }

        private void SetErrorMessage()
        {
            TempData["ErrorMessage"] = "Lo siento, hubo un problema con los servidores, " +
                                       "inténtelo más tarde por favor, si el error persiste, " +
                                       "comuníquese con el personal!";
        }
    }
}
