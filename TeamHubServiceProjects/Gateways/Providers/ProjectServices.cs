
using MySql.Data.MySqlClient;
using TeamHubServiceProjects.DTOs;
using TeamHubServiceProjects.Entities;
using TeamHubServiceProjects.Gateways.Interfaces;

namespace TeamHubServiceProjects.Gateways.Providers;

public class ProjectServices : IProjectServices
{
    private TeamHubContext dbContext;
    private readonly ILogger<ProjectServices> _logger;
    public ProjectServices(TeamHubContext dbContext, ILogger<ProjectServices> logger)
    {
        this.dbContext = dbContext;
        _logger = logger;
    }

    public bool AddProject(project projectNew, int studentID)
    {
        bool result;

        try
        {
            dbContext.project.Add(projectNew);
            dbContext.SaveChanges();
            projectstudent projectstudentaux = new projectstudent();
            projectstudentaux.IdStudent = studentID;
            projectstudentaux.IdProject = projectNew.IdProject;
            dbContext.projectstudent.Add(projectstudentaux);
            dbContext.SaveChanges();
            result = true;
        }
        catch (MySqlException sqlEx)
        {
            result = false;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = false;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = false;
            _logger.LogError(ex.Message);
        }

        return result;
    }

    public bool DeleteProject(int projectId)
    {
        bool result = false;

        try
        {
            var projectDB = dbContext.project.Find(projectId);
            if (projectDB != null)
            {
                var listTask = dbContext.tasks.Where(t => t.IdProject == projectId).ToList();
                dbContext.tasks.RemoveRange(listTask);
                var listDocument = dbContext.document.Where(d => d.IdProject == projectId).ToList();
                dbContext.document.RemoveRange(listDocument);
                dbContext.project.Remove(projectDB);
                dbContext.SaveChanges();
                result = true;
            }
        }
        catch (MySqlException sqlEx)
        {
            result = false;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = false;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = false;
            _logger.LogError(ex.Message);
        }

        return result;
    }

    public List<project> GetAllProjectsByStudentID(int studentID)
    {
        List<project> result = new List<project>();

        try
        {
            result = dbContext.project
                .Where(p => p.projectstudent.Any(ps => ps.IdStudent == studentID))
                .ToList();
        }
        catch (MySqlException sqlEx)
        {
            result = null;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = null;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = null;
            _logger.LogError(ex.Message);
        }

        return result;
    }

    public bool UpdateProject(project projectUpdate)
    {
        bool result = false;

        try
        {
            var projectDB = dbContext.project.Find(projectUpdate.IdProject);
            if (projectDB != null)
            {
                projectDB.Name = projectUpdate.Name;
                projectDB.StartDate = projectUpdate.StartDate;
                projectDB.EndDate = projectUpdate.EndDate;
                projectDB.Status = projectUpdate.Status;
                dbContext.project.Update(projectDB);
                dbContext.SaveChanges();
                result = true;
            }
        }
        catch (MySqlException sqlEx)
        {
            result = false;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = false;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = false;
            _logger.LogError(ex.Message);
        }

        return result;
    }

    public project GetProjectByID(int projectId)
    {
        project result = new project();

        try
        {
            result = dbContext.project.Find(projectId);
        }
        catch (MySqlException sqlEx)
        {
            result = null;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = null;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = null;
            _logger.LogError(ex.Message);
        }

        return result;
    }

    public List<TaskDTO> GetTasksByProject(int idProject)
    {
        List<TaskDTO> result = new List<TaskDTO>();
        try
        {
            result = dbContext.tasks
                .Where(t => t.IdProject == idProject)
                .Select(t => new TaskDTO
                {
                    IdTask = t.IdTask,
                    Name = t.Name,
                    Description = t.Description,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    IdProject = t.IdProject,
                    Status = t.Status
                })
                .ToList();
        }
        catch (MySqlException sqlEx)
        {
            result = null;
            _logger.LogError(sqlEx.Message);
        }
        catch (TimeoutException timeoutEx)
        {
            result = null;
            _logger.LogError(timeoutEx.Message);
        }
        catch (Exception ex)
        {
            result = null;
            _logger.LogError(ex.Message);
        }

        return result;
    }
}