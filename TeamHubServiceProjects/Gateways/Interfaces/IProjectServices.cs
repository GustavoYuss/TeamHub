
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;

namespace TeamHubServiceProjects.Gateways.Interfaces;

public interface IProjectServices 
{
    public bool AddProject(project projectNew, int studentID);
    public bool UpdateProject(project projectUpdate);
    public bool DeleteProject(int projectId);
    public List<project> GetAllProjectsByStudentID(int studentID);
    public project GetProjectByID(int projectId);
    public List<TaskDTO> GetTasksByProject(int idProject); 
}