
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IProjectManager
    {
        public Task<bool> AddProject(ProjectDTO project, int studentID);
        public Task<bool> UpdateProject(ProjectDTO projectNew);
        public Task<bool> RemoveProject(int projectID);
        public ProjectDTO GetProject(int idProject);
        public  List<ProjectDTO> GetAllMyProjects(int idStudent);
        public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate);
        public List<TaskDTO> GetProjectTasksAsync(int idProject);
    }
}