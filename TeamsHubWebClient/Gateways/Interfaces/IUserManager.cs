using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces 
{
    public interface IUserManager 
    {
        public List<User> GetStudentsByProject(int idProject);
        public bool AddStudent(StudentDTO newStudent);
        public Task<int> EditStudent(StudentDTO editStudent);
        public List<User> SearchStudent(string student);
        public bool DeleteStudent(int idProject, int idStudent);
        public bool AddStudentToProject(int idProject, int idStudent);
        public bool PasswordRecovery(string userEmail);
        public StudentDTO GetUserPersonalData(int studentID);
    }
}
