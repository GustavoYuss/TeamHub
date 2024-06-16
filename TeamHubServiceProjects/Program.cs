using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;
using TeamHubServiceProjects.Gateways.Providers;
using TeamHubServiceProjects.UseCases.Interfaces;
using TeamHubServiceProjects.UseCases.Providers;
using TeamHubServiceUser.Gateways.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var config = builder.Configuration;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    var llave = config["JWTSettings:Key"];
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = config["JWTSettings:Issuer"],
        ValidAudience = config["JWTSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(llave)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<IProjectManagement, ProjectManagement>();
builder.Services.AddScoped<IProjectServices, ProjectServices>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddDbContext<TeamHubContext>(options =>
{
    var connectionString = builder.Configuration
                           .GetConnectionString("MySQLCursos") ?? "DefaultConnectionString";
    options.UseMySQL(connectionString);
});

builder.Services.AddAuthorization();
/*
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("usuario_valido", policy =>
        policy
            .RequireRole("Administrador")
            .RequireClaim("scope", "CursosAPP"));
builder.Services.AddCors();
*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/TeamHub/Projects/MyProjects/{studentID}", (IProjectManagement projectManagement, int studentID, ILogService LogService, HttpContext httpContext) =>
{
    if (!InputValidator.IsValidId(studentID))
    {
        return Results.BadRequest("Invalid student ID");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Obtener Lista de proyectos"
    });

    var projects = projectManagement.GetAllProjectsByStuden(studentID);
    return projects != null ? Results.Ok(projects) : Results.Problem("Error Server");
})
.WithName("GetListaProyectos")
.RequireAuthorization()
.WithOpenApi();

app.MapPost("/TeamHub/Projects/AddProject", (IProjectManagement projectManagement, AddProjectRequestDTO request, ILogService LogService, HttpContext httpContext) =>
{
    if (request == null || !InputValidator.IsValidString(request.ProjectNew.Name) || !InputValidator.IsValidId(request.StudentID))
    {
        return Results.BadRequest("Invalid request data");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Crear nuevo proyecto"
    });

    var response = projectManagement.AddProject(request.ProjectNew, request.StudentID);
    return response ? Results.Ok(response) : Results.Problem("Error Server");
})
.WithName("AgregarProyecto")
.RequireAuthorization()
.WithOpenApi();

app.MapPut("/TeamHub/Projects/UpdateProject", (IProjectManagement projectManagement, project projectUpdate, ILogService LogService, HttpContext httpContext) =>
{
    if (projectUpdate == null || !InputValidator.IsValidId(projectUpdate.IdProject))
    {
        return Results.BadRequest("Invalid project data");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Actualizar Proyecto"
    });

    var response = projectManagement.UpdateProject(projectUpdate);
    return response ? Results.Ok(response) : Results.Problem("Error Server");
})
.WithName("UpdateProject")
.RequireAuthorization()
.WithOpenApi();

app.MapDelete("/TeamHub/Projects/DeleteProject", (IProjectManagement projectManagement, int idProject, ILogService LogService, HttpContext httpContext) =>
{
    if (!InputValidator.IsValidId(idProject))
    {
        return Results.BadRequest("Invalid project ID");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Eliminar Proyecto"
    });

    var response = projectManagement.RemoveProject(idProject);
    return response ? Results.Ok(response) : Results.Problem("Error Server");
})
.WithName("DeleteProject")
.RequireAuthorization()
.WithOpenApi();

app.MapGet("/TeamHub/Projects/Project/{idProject}", (IProjectManagement projectManagement, int idProject, ILogService LogService, HttpContext httpContext) =>
{
    if (!InputValidator.IsValidId(idProject))
    {
        return Results.BadRequest("Invalid project ID");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Obtener Informacion de Proyecto especifico"
    });

    var project = projectManagement.GetProjectByID(idProject);
    return project != null ? Results.Ok(project) : Results.Problem("Error Server");
})
.WithName("GetProject")
.RequireAuthorization()
.WithOpenApi();

app.MapGet("/TeamHub/Projects/Project/Tasks/{idProject}", (IProjectManagement projectManagement, int idProject, ILogService LogService, HttpContext httpContext) =>
{
    if (!InputValidator.IsValidId(idProject))
    {
        return Results.BadRequest("Invalid project ID");
    }
    if (!InputValidator.IsValidRequest(httpContext, out int idUserClaim, out int idSessionClaim))
    {
        return Results.Unauthorized();
    }

    LogService.SaveUserAction(new UserActionDTO()
    {
        IdUser = idUserClaim,
        IdUserSession = idSessionClaim,
        Action = "Obtener Tareas de proyecto"
    });

    var tasks = projectManagement.GetTasksByProject(idProject);
    return tasks != null ? Results.Ok(tasks) : Results.Problem("Error Server");
})
.WithName("GetProjectTasks")
.RequireAuthorization()
.WithOpenApi();

app.Run();

