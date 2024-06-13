using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces 
{
    public interface IUserManager 
    {
        public List<User> GetStudentsByProject(int idProject);
        public bool AddStudent(StudentDTO newStudent);
        public bool EditStudent(StudentDTO editStudent);
        public List<User> SearchStudent(string student);
        public bool DeleteStudent(int idProject, int idStudent);
        public bool AddStudentToProject(int idProject, int idStudent);
    }
}
