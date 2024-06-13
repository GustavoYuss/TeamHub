namespace TeamHubServiceProjects.Tests;
using System;
using System.Text;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;

public class TestProjectModule
{
    public class Project
    {
        public int IdProject { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
    }

    public class AddProjectRequestDTO
    {
        public Project ProjectNew { get; set; }
        public int StudentID { get; set; }
    }


    [Fact]
    public static async Task TestAddProjectValid()
    {
        HttpClient client = new HttpClient();   
        client.DefaultRequestHeaders.Add("accept", "application/json");         
        client.BaseAddress = new Uri("http://localhost:8081");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6IkNhbW9AZ21haWwuY29tIiwianRpIjoiYTUwYjYyMmMtYzUyYi00ZjcwLWI5Y2MtNGVkODVjMTA4NThlIiwiYmlydGhkYXRlIjoiMDYvMTIvMjAyNCAxODo1MzowMCIsInNjb3BlIjoiVGVhbUh1YkFwcCIsIklkVXNlciI6IjIiLCJJZFNlc3Npb24iOiI0IiwibmFtZSI6IkFuZ2VsTWlndWVsQ2Ftb1JpbmNvbiIsIm5hbWVpZCI6IkNhbW9AZ21haWwuY29tIiwibmJmIjoxNzE4MjE4MzgwLCJleHAiOjE3MjY4Nzk5ODAsImlhdCI6MTcxODIxODM4MCwiaXNzIjoiaHR0cDovLzE3Mi4xNi4wLjc6ODA4MCIsImF1ZCI6Imh0dHA6Ly8xNzIuMTYuMC41OjgwODAsIGh0dHA6Ly8xNzIuMTYuMC43OjgwODAsICBodHRwOi8vMTcyLjE2LjAuMjo4MDgwLCBodHRwOi8vMTcyLjE2LjAuMzo4MDgwLCBodHRwOi8vMTcyLjE2LjAuNDo4MDgwLCBodHRwOi8vMTcyLjE2LjAuNjo4MDgwICJ9.5awVIPAyOm7OXyRSZsgXi8MDY0duE2ewf8_tULidTdo");

        Project projectTest = new Project(){
            Name = "Prueba del sistema 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Status = "PROBANDO"
        };

        AddProjectRequestDTO projectRequest = new AddProjectRequestDTO(){
            ProjectNew = projectTest,
            StudentID = 1
        };

        var result = client.PostAsJsonAsync("/TeamHub/Projects/AddProject", projectRequest).Result;

        result.EnsureSuccessStatusCode();
        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }

    [Fact]
    public static async Task TestAddProjectWithNullName()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8081");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = null,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "PROBANDO"
        };

        AddProjectRequestDTO projectRequest = new AddProjectRequestDTO(){
            ProjectNew = projectTest,
            StudentID = 1
        };

        var result = await client.PostAsJsonAsync("/TeamHub/Projects/AddProject", projectRequest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithInvalidDates()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8081");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con fechas no válidas",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
            Status = "PROBANDO"
        };

        AddProjectRequestDTO projectRequest = new AddProjectRequestDTO(){
            ProjectNew = projectTest,
            StudentID = 1
        };

        var result = await client.PostAsJsonAsync("/TeamHub/Projects/AddProject", projectRequest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithNullStatus()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8081");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con estado nulo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = null
        };

        AddProjectRequestDTO projectRequest = new AddProjectRequestDTO(){
            ProjectNew = projectTest,
            StudentID = 1
        };

        var result = await client.PostAsJsonAsync("/TeamHub/Projects/AddProject", projectRequest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithInvalidToken()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8081");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token_invalido");

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con token inválido",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "PROBANDO"
        };

        AddProjectRequestDTO projectRequest = new AddProjectRequestDTO(){
            ProjectNew = projectTest,
            StudentID = 1
        };

        var result = await client.PostAsJsonAsync("/TeamHub/Projects/AddProject", projectRequest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }
}