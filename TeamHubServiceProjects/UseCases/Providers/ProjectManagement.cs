

using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;
using TeamHubServiceProjects.UseCases.Interfaces;

namespace TeamHubServiceProjects.UseCases.Providers
{
    public class ProjectManagement : IProjectManagement
    {
        private IProjectServices projectServices;

        public ProjectManagement(IProjectServices projectServices)
        {
            this.projectServices = projectServices;
        }

        public bool AddProject(project project, int studentID)
        {
            project projectNew = new project()
            {
                IdProject = 0,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };
            return projectServices.AddProject(projectNew, studentID);
        }

        public bool RemoveProject(int projectID)
        {
            return projectServices.DeleteProject(projectID);
        }

        public bool UpdateProject(project projectUpdate)
        {
            project projectNew = new project()
            {
                IdProject = projectUpdate.IdProject,
                Name = projectUpdate.Name,
                StartDate = projectUpdate.StartDate,
                EndDate = projectUpdate.EndDate,
                Status = projectUpdate.Status
            };
            return projectServices.UpdateProject(projectNew);
        }

        public project GetProjectByID(int projectId)
        {
            return projectServices.GetProjectByID(projectId);
        }

        public List<TaskDTO> GetTasksByProject(int idProject)
        {
            return projectServices.GetTasksByProject(idProject);
        }

        public List<project> GetAllProjectsByStuden(int studenId)
        {
            return projectServices.GetAllProjectsByStudentID(studenId);
        }
    }
}