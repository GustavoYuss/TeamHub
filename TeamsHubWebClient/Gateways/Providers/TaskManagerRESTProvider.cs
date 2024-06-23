

using System.Text;
using System.Text.Json;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

public class TaskManagerRESTProvider : ITaskManager
{
    HttpClient clientServiceTask;
    ILogger<TaskManagerRESTProvider> _logger;

    public TaskManagerRESTProvider(ILogger<TaskManagerRESTProvider> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        clientServiceTask = httpClientFactory.CreateClient("ApiGateWay");
    }

    public async Task<bool> AddTask(TaskDTO newTask)
    {
        bool response;

        try
        {
            var json = JsonSerializer.Serialize(newTask);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/TeamHub/Task/") { Content = content };
            var result = await clientServiceTask.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = true;
            }
            else
            {
                response = false;
            }
        }
        catch (Exception ex)
        {
            response = false;
        }

        return response;
    }

    public async Task<bool> UpdateTask(TaskDTO task)
    {
        bool response;
        try
        {
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "/TeamHub/Task/up") { Content = content };
            var result = await clientServiceTask.SendAsync(request);
            if (result.IsSuccessStatusCode)
            {
                response = true;
            }
            else
            {
                response = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            response = false;
        }

        return response;
    }

    public List<TaskDTO> GetAllTaskByProject(int projectID)
    {
        try
        {
            var result = clientServiceTask.GetAsync($"/TeamHub/Task/{projectID}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<TaskDTO>>().Result;
            return response;
        }
        catch (Exception ex)
        {

            return null;
        }
    }


    public List<TaskDTO> GetTaskbyDate(DateTime startDate, DateTime endDate)
    {
        try
        {
            var startDateFormat = $"{startDate.Year}-{startDate.Month:00}-{startDate.Day:00}";
            var endDateFormat = $"{endDate.Year}-{endDate.Month:00}-{endDate.Day:00}";
            var result = clientServiceTask.GetAsync($"/TeamHub/Task/{startDateFormat}/{endDateFormat}").Result;
            result.EnsureSuccessStatusCode();
            var response = result.Content.ReadFromJsonAsync<List<TaskDTO>>().Result;
            return response;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public bool RemoveTask(int taskID)
    {
        return true;
    }
}