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

app.MapPost("/TeamHub/Users/", async (IUserService userService, ILogService logService, HttpContext httpContext, StudentDTO newStudent) =>
{
    try
    {
        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = 0,
            IdUserSession = 0,
            Action = "Agregar Estudiante"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var addResult = userService.AddStudent(newStudent);
        return Results.Ok(addResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("AddUser")
.WithOpenApi();



app.MapPost("/TeamHub/Users/Delete", async (IUserService userService, ILogService logService, HttpContext httpContext, int idDeleteStudent) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Eliminar Estudiante"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var deleteUserResult = userService.DeleteStudent(idDeleteStudent);
        return Results.Ok(deleteUserResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest(-1);
    }
})
.WithName("DeleteUser")
.RequireAuthorization()
.WithOpenApi();

app.MapPut("/TeamHub/Users/Edit", async (IUserService userService, ILogService logService, HttpContext httpContext, StudentDTO editStudent) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Editar Estudiante"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var editUserResult = userService.EditStudent(editStudent);
        return Results.Ok(editUserResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("EditUser")
.RequireAuthorization()
.WithOpenApi();



app.MapGet("/TeamHub/Users/ByProject/{idProject}", async (IUserService userService, ILogService logService, HttpContext httpContext, int idProject) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Obtener Estudiantes de un proyecto"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var students = userService.GetStudentByProject(idProject);
        return Results.Json(students);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("GetUserByProject")
.RequireAuthorization()
.WithOpenApi();


app.MapGet("/TeamHub/Users/GetUserInformation/{idUser}", async (IUserService userService, ILogService logService, HttpContext httpContext, int idUser) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUserLogged = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUserLogged,
            IdUserSession = idSession,
            Action = "Obtener informacion de un Estudiante de un proyecto"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var student = userService.GetStudentInfo(idUser);
        return Results.Json(student);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("GetUserInformation")
.RequireAuthorization()
.WithOpenApi();


app.MapDelete("/TeamHub/Users/RemoveOfProject/{idProject}/{idStudent}", async (IUserService userService, ILogService logService, HttpContext httpContext, int idProject, int idStudent) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Remover Estudiante de un proyecto"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var removeFromProjectResult = userService.RemoveStudentFromProject(idStudent, idProject);
        return Results.Ok(removeFromProjectResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("RemoveStudentProject")
.RequireAuthorization()
.WithOpenApi();


app.MapPost("/TeamHub/Users/AddToProject/{idProject}/{idStudent}", async (IUserService userService, ILogService logService, HttpContext httpContext, int idProject, int idStudent) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Añadir Estudiante a un proyecto"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var addStudentResult = userService.AddStudentToProject(idStudent, idProject);
        return Results.Ok(addStudentResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("AddStudentToProject")
.RequireAuthorization()
.WithOpenApi();


app.MapGet("/TeamHub/Users/Search/{student}", async (IUserService userService, ILogService logService, HttpContext httpContext, string student) =>
{
    try
    {
        var idUserClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;
        var idSessionClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IdSession")?.Value;

        if (idUserClaim == null || idSessionClaim == null)
        {
            return Results.Unauthorized();
        }

        int idUser = int.Parse(idUserClaim);
        int idSession = int.Parse(idSessionClaim);

        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = idUser,
            IdUserSession = idSession,
            Action = "Buscar Estudiante especifico"
        });

        if (result != 1)
        {
            return Results.StatusCode(500); // Internal Server Error
        }

        var students = userService.SearchStudents(student);
        return Results.Json(students);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("SearchStudent")
.RequireAuthorization()
.WithOpenApi();


app.MapGet("/TeamHub/Users/RecoveryPassword/{userEmail}", async (IUserService userService, ILogService logService, HttpContext httpContext, string userEmail) =>
{
    try
    {
        int result = logService.SaveUserAction(new UserActionDTO
        {
            IdUser = 0,
            IdUserSession = 0,
            Action = "Recuperar contraseña"
        });

        if (result != 1)
        {
            return Results.StatusCode(500);
        }
        
        var passwordResult = userService.RecoverUserPassword(userEmail);
        return Results.Ok(passwordResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return Results.BadRequest();
    }
})
.WithName("RecoveryPassword")
.WithOpenApi();


app.Run();
