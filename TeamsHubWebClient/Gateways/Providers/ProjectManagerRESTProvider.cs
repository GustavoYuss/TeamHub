using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;
using System.Text;
using System.Text.Json;

public class ProjectManagerRESTProvider : IProjectManager
{
    HttpClient clientServiceProjects;
    ILogger<ProjectManagerRESTProvider> _logger;

    public ProjectManagerRESTProvider(
        ILogger<ProjectManagerRESTProvider> logger,
        IHttpClientFactory httpClientFactory) 
    {
            _logger = logger;
            clientServiceProjects = httpClientFactory.CreateClient("ApiGateWay");
    } 

    public bool AddProject(ProjectDTO project, int idStudent)
    {
        try
        {
            var request = new { ProjectNew = project, StudentID = idStudent };
            var result = clientServiceProjects.PostAsJsonAsync($"/TeamHub/Projects/AddProject", request).Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public List<ProjectDTO> GetAllMyProjects(int idStudent)
    {
        try
        {
            var result = clientServiceProjects.GetAsync($"/TeamHub/Projects/MyProjects/{StudentSinglenton.Id}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<ProjectDTO>>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public ProjectDTO GetProject(int idProject)
    {
        try
        {
            var result = clientServiceProjects.GetAsync($"/TeamHub/Projects/Project/{idProject}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<ProjectDTO>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public List<ProjectDTO> GetProjectsbyDate(DateTime startDate, DateTime endDate)
    {
        try {                        
            var startDateFormat = $"{startDate.Year}-{startDate.Month:00}-{startDate.Day:00}";
            var endDateFormat = $"{endDate.Year}-{endDate.Month:00}-{endDate.Day:00}";
            var result = clientServiceProjects.GetAsync($"/TeamHub/Projects/MyProjects/{startDateFormat}/{endDateFormat}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<ProjectDTO>>().Result;    
            return response;
        } catch (Exception e) {
            return null;
        }
    }
    public bool RemoveProject(ProjectDTO project)
    {
        return true;
    }
    public bool UpdateProject(ProjectDTO projectNew)
    {
        try
        {
            var result = clientServiceProjects.PostAsJsonAsync($"/TeamHub/Projects/UpdateProject", projectNew).Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
            return response;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public List<TaskDTO>? GetProjectTasksAsync(int idProject)
    {
        List<TaskDTO>? taskList = new List<TaskDTO>();
        try
        {
            var result = clientServiceProjects.GetAsync($"/TeamHub/Projects/Project/Tasks/{idProject}").Result;            
            result.EnsureSuccessStatusCode();
            taskList = result.Content.ReadFromJsonAsync<List<TaskDTO>>().Result;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        return taskList;
    }
}