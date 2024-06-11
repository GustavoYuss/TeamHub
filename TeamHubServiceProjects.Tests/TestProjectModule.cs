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


    [Fact]
    public static async Task TestAddProjectValid()
    {
        HttpClient client = new HttpClient();   
        client.DefaultRequestHeaders.Add("accept", "application/json");         
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6Inl1c2d1czAyQGdtYWlsLmNvbSIsImp0aSI6IjdhMzA3NTdjLWY0MWQtNGNjZC1hODZmLWRkOGUwMmRkOGE2ZCIsImJpcnRoZGF0ZSI6IjA2LzA3LzIwMjQgMDE6MTg6NDgiLCJzY29wZSI6IlRlYW1IdWJBcHAiLCJJZFVzZXIiOiI0IiwiSWRTZXNzaW9uIjoiNyIsIm5hbWUiOiJZdXNzaWZHdXN0YXZvTWVuZG96YVNldmVybyIsIm5hbWVpZCI6Inl1c2d1czAyQGdtYWlsLmNvbSIsIm5iZiI6MTcxNzc0NDcyOCwiZXhwIjoxNzI2NDA2MzI4LCJpYXQiOjE3MTc3NDQ3MjgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA4MSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6ODA4MCJ9.MX3uNInEvWtArniTgLptjcyiGXEegNPKvDjUScTnJA4");

        Project projectTest = new Project(){
            Name = "Prueba del sistema 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Status = "PROBANDO"
        };

        var result = client.PostAsJsonAsync<Project>
                    ("/Projects", projectTest).Result;

        result.EnsureSuccessStatusCode();
        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
    }

    [Fact]
    public static async Task TestAddProjectWithNullName()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = null,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "PROBANDO"
        };

        var result = await client.PostAsJsonAsync("/Projects", projectTest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithInvalidDates()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con fechas no válidas",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now,
            Status = "PROBANDO"
        };

        var result = await client.PostAsJsonAsync("/Projects", projectTest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithNullStatus()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."); // Token válido

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con estado nulo",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = null
        };

        var result = await client.PostAsJsonAsync("/Projects", projectTest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

    [Fact]
    public static async Task TestAddProjectWithInvalidToken()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("accept", "application/json");
        client.BaseAddress = new Uri("http://localhost:8080");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token_invalido");

        Project projectTest = new Project()
        {
            Name = "Prueba del sistema con token inválido",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Status = "PROBANDO"
        };

        var result = await client.PostAsJsonAsync("/Projects", projectTest);

        string responseContent = await result.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        Assert.False(result.IsSuccessStatusCode);
    }

}