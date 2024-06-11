
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;

namespace TeamHubServiceProjects.UseCases.Interfaces
{
    public interface IProjectManagement
    {
        public List<project> GetAllProjectsByStuden(int studenId);
        public bool AddProject(project project, int studentID);
        public bool RemoveProject(int projectID);
        public bool UpdateProject(project projectUpdate);
        public project GetProjectByID(int projectId);
        public List<TaskDTO> GetTasksByProject(int idProject); 
    }
}