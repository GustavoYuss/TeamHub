
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IProjectManager
    {
        public bool AddProject(ProjectDTO project, int studentID);
        public bool UpdateProject(ProjectDTO projectNew);
        public bool RemoveProject(ProjectDTO project);
        public ProjectDTO GetProject(int idProject);
        public  List<ProjectDTO> GetAllMyProjects(int idStudent);
        public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate);
        public List<TaskDTO> GetProjectTasksAsync(int idProject);
    }
}