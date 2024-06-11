using TeamHubServiceUser.Entities;
using TeamHubServiceUser.Gateways.Interfaces;
using TeamHubServiceUser.DTOs;

namespace TeamHubServiceUser.Gateways.Providers;

public class UserService : IUserService
{

    private TeamHubContext dbContext;

    public UserService(TeamHubContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public int AddStudent(StudentDTO newStudent)
    {
        int result = 0;
        try
        {
            var dbStudent = new student
            {
                IdStudent = 0,
                Name = newStudent.Name,
                MiddleName = newStudent.MiddleName,
                LastName = newStudent.LastName,
                SurName = newStudent.SurName,
                Email = newStudent.Email,
                Password = newStudent.Password,
                ProDocumentImage = newStudent.ProDocumentImage
            };

            dbContext.student.Add(dbStudent);
            result = dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }

    public int DeleteStudent(int IdDeleteStudent)
    {
        int result = 0;
        try
        {
            var dbStudent = dbContext.student.Find(IdDeleteStudent);
            if (dbStudent != null)
            {
                dbContext.student.Remove(dbStudent);
                result = dbContext.SaveChanges();
            }
            else
            {
                result = -1;
            }
            result = dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }

    public int EditStudent(StudentDTO editStudent)
    {
        int result = 0;
        try
        {

            var dbStudent = dbContext.student.Find(editStudent.IdStudent);
            if (dbStudent != null)
            {
                dbStudent.IdStudent = editStudent.IdStudent;
                dbStudent.Name = editStudent.Name;
                dbStudent.MiddleName = editStudent.MiddleName;
                dbStudent.LastName = editStudent.LastName;
                dbStudent.SurName = editStudent.SurName;
                dbStudent.Email = editStudent.Email;
                dbStudent.Password = editStudent.Password;
                dbStudent.ProDocumentImage = editStudent.ProDocumentImage;
                dbContext.student.Update(dbStudent);
                result = dbContext.SaveChanges();
            }
            else
            {
                result = -1;
            }
        }
        catch (Exception ex)
        {
            
            result = 1;
        }

        return result;
    }

    public List<UserDTO> GetStudentByProject(int IdProject)
    {
        List<UserDTO> UserList = new List<UserDTO>();
        try
        {
            var projectStudents = dbContext.projectstudent
                .Where(p => p.IdProject == IdProject)
                .Join(dbContext.student,
                    project => project.IdStudent,
                    student => student.IdStudent,
                    (project, student) => new
                    {
                        Student = student
                    })
                .ToList();

            foreach (var item in projectStudents)
            {
                UserDTO studentTransfer = new UserDTO
                {
                    Id = item.Student.IdStudent,
                    FullName = $"{item.Student.Name} {item.Student.MiddleName} {item.Student.LastName} {item.Student.SurName}",
                    Email = item.Student.Email
                };
                UserList.Add(studentTransfer);
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
        return UserList;
    }

    public int RemoveStudentFromProject(int IdStudent, int IdProject)
    {
        int result = 0;
        try
        {
            var dbproject = dbContext.projectstudent.Where(p => p.IdProject == IdProject && p.IdStudent == IdStudent).FirstOrDefault();
            if (dbproject != null)
            {
                dbContext.Remove(dbproject);
                result = dbContext.SaveChanges();
            }
        }   
        catch (System.Exception)
        {
            
            throw;
        }

        return result;
    }

    public int AddStudentToProject(int IdStudent, int IdProject)
    {
        int result = 0;
        try
        {
            var dbproject = dbContext.projectstudent.Where(p => p.IdProject == IdProject && p.IdStudent == IdStudent).FirstOrDefault();
            if(dbproject == null)
            {
                projectstudent projectstudentDB = new projectstudent
                {
                    IdProject = IdProject,
                    IdStudent = IdStudent
                };

                dbContext.projectstudent.Add(projectstudentDB);
                result = dbContext.SaveChanges();
            }

        }   
        catch (System.Exception)
        {
            
            throw;
        }

        return result;
    }

    public List<UserDTO> SearchStudents(string student)
    {
        List<UserDTO> listStudents = new List<UserDTO>();
        if (!string.IsNullOrWhiteSpace(student))
        {
            student = student.ToUpper();

            try
            {
                listStudents = dbContext.student
                                .Where(s => 
                                    (s.Name != null && s.Name.ToLower().Contains(student)) ||
                                    (s.MiddleName != null && s.MiddleName.ToLower().Contains(student)) ||
                                    (s.LastName != null && s.LastName.ToLower().Contains(student)) ||
                                    (s.SurName != null && s.SurName.ToLower().Contains(student)) ||
                                    (s.Email != null && s.Email.ToLower().Contains(student))
                                )
                                .Take(10) 
                                .Select(s => new UserDTO
                                {
                                    Id = s.IdStudent,
                                    Email = s.Email,
                                    FullName = $"{s.Name} {s.MiddleName} {s.LastName} {s.SurName}"
                                })
                                .ToList();
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        return listStudents;
    }
}