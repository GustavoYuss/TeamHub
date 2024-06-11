using TeamHubServiceUser.Entities;
using TeamHubServiceUser.DTOs;

namespace TeamHubServiceUser.Gateways.Interfaces;

public interface IUserService
{
    public int AddStudent(StudentDTO newStudent);
    public int EditStudent(StudentDTO editStudent);
    public int DeleteStudent(int IdDeleteStudent);
    public List<UserDTO> GetStudentByProject(int IdProject);
    public int RemoveStudentFromProject(int IdStudent, int IdProject);
    public int AddStudentToProject(int IdStudent, int IdProject);
    public List<UserDTO> SearchStudents(string student);
}