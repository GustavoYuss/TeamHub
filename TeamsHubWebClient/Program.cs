using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.Gateways.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddScoped<IUserIdentityManager, UserIdentityManagerRESTProvider>();
builder.Services.AddScoped<IProjectManager, ProjectManagerRESTProvider>();
builder.Services.AddScoped<ITaskManager, TaskManagerRESTProvider>();
builder.Services.AddScoped<IUserManager, UserManagerRESTProvider>();
builder.Services.AddScoped<IFileManager, FileManagerRestProvider>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<HttpClientsAuthHelper>();


builder.Services.AddHttpClient("ApiGateWay", client => {
    var apiGateWayUrl = builder.Configuration.GetSection("Services")["apiGateWay"];
    client.BaseAddress = new Uri(apiGateWayUrl);
    client.DefaultRequestHeaders.Add("accept", "application/json");
}).AddHttpMessageHandler<HttpClientsAuthHelper>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".TeamsHub.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapGet("/TeamHub/Projects/MyProjects/{startDate?}/{endDate?}", (IProjectManager proyectManager, DateTime? startDate, DateTime? endDate) =>
{     
    if(startDate != null && endDate != null){
        return new { data = proyectManager.GetProjectsbyDate(startDate.Value, endDate.Value)};
    }
    else
    {
        return null;
    }

}).WithName("myProjectsbyDate");

app.MapGet("/TeamHub/Projects/MyProjects/{idStudent}", (IProjectManager proyectManager, int idStudent) =>
{     
        return new { data = proyectManager.GetAllMyProjects(idStudent)};
        
}).WithName("myProjects");

app.MapGet("/TeamHub/Projects/{idProject}", (IProjectManager proyectManager, int idProject) =>
{     
        return new { data = proyectManager.GetProject(idProject)};

}).WithName("obtenerProyecto");

app.MapGet("/TeamHub/Task/{IdProject}", ([FromServices] ITaskManager taskManager, [FromRoute] int IdProject) =>
{     
        return new { data = taskManager.GetAllTaskByProject(IdProject)};

}).WithName("GetAllTaskByProject");

app.MapPost("/TeamHub/Task/", (ITaskManager taskManager, TaskDTO taskDTO) =>
{            
    bool result = false;

    if (taskDTO.IdTask == 0)
    {
        taskManager.AddTask(taskDTO);
        result = true;
    }

    return result;     

}).WithName("AgregarTarea");

app.MapPost("/TeamHub/Task/up", (ITaskManager taskManager, TaskDTO taskDTO) =>
{            
    bool result = false;

    if (taskDTO.IdTask != 0)
    {
        taskManager.UpdateTask(taskDTO);
        result = true;
    }    

    return result;       
}).WithName("ModificarTarea");

app.MapRazorPages();
app.Run();

public sealed class HttpClientsAuthHelper : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;

    public HttpClientsAuthHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _accessor.HttpContext.Request.HttpContext.Session.GetString("token_usuario");
        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            Console.WriteLine("Token is null");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
