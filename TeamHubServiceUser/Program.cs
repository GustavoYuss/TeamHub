using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeamHubServiceUser.Gateways.Interfaces;
using TeamHubServiceUser.Gateways.Providers;
using TeamHubServiceUser.Entities;
using TeamHubServiceUser.DTOs;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

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

// Agrega el servicio de autorización
builder.Services.AddAuthorization();

builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<TeamHubContext>(options => {
    var connectionString = builder.Configuration
                           .GetConnectionString("MySQLCursos")?? "DefaultConnectionString";
    options.UseMySQL(connectionString);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/TeamHub/Users/", (IUserService userService, ILogService LogService, HttpContext httpContext, StudentDTO newStudent) => 
{
    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = 0,
            IdUserSession = 0,
            Action = "Añadir Estudiante"
        }
    );
    return userService.AddStudent(newStudent);
})
.WithName("AddUser")
.RequireAuthorization()
.WithOpenApi();


app.MapPost("/TeamHub/Users/Delete", (IUserService userService, ILogService LogService, HttpContext httpContext, int idDeleteStudent) => 
{
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault()); 

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Eliminar Estudiante"
        }
    );
    return userService.DeleteStudent(idDeleteStudent);
})
.WithName("DeleteUser")
.RequireAuthorization()
.WithOpenApi();

app.MapPost("/TeamHub/Users/Edit", (IUserService userService, ILogService LogService, HttpContext httpContext, StudentDTO editStudent) => 
{
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault()); 

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Editar Estudiante"
        }
    );
    return userService.EditStudent(editStudent);
})
.WithName("EditeUser")
.RequireAuthorization()
.WithOpenApi();


app.MapGet("/TeamHub/Users/ByProject/{idProject}", (IUserService userService, ILogService LogService, HttpContext httpContext, int idProject) => 
{
    var students = userService.GetStudentByProject(idProject);
        
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault()); 

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Obtener Usuarios de un proyecto"
        }
    );
    return Results.Json(students);
})
.WithName("GetUserByProject")
.RequireAuthorization()
.WithOpenApi();

app.MapDelete("/TeamHub/Users/RemoveOfProject/{idProject}/{idStudent}", (IUserService userService, ILogService LogService, HttpContext httpContext, int idProject, int idStudent) => 
{
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault());

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Remover estudiante de proyecto"
        }
    );
    return userService.RemoveStudentFromProject(idStudent,idProject);
})
.WithName("RemoveStudentProject")
.RequireAuthorization()
.WithOpenApi();


app.MapPost("/TeamHub/Users/AddToProject/{idProject}/{idStudent}", (IUserService userService, ILogService LogService, HttpContext httpContext, int idProject, int idStudent) => 
{
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault()); 

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Añadir estudiante a proyecto"
        }
    );
    return userService.AddStudentToProject(idStudent,idProject);
})
.WithName("AddStudentToProject")
.RequireAuthorization()
.WithOpenApi();

app.MapGet("/TeamHub/Users/Search/{student}", (IUserService userService, ILogService LogService, HttpContext httpContext, string student) => 
{
    int idUserClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdUser").Select(c=>c.Value ).SingleOrDefault()); 
    int idSessionClaim = Int32.Parse(httpContext.User.Claims.Where(c => c.Type == "IdSession").Select(c=>c.Value ).SingleOrDefault()); 

    LogService.SaveUserAction(
        new UserActionDTO() {
            IdUser = idUserClaim,
            IdUserSession = idSessionClaim,
            Action = "Buscar estudiante especifico"
        }
    );
    var students = userService.SearchStudents(student);
    return Results.Json(students);
})
.WithName("SearchStudent")
.RequireAuthorization()
.WithOpenApi();

app.Run();
