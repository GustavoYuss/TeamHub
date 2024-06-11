using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces 
{

    public interface IUserManager 
    {
        public List<User> getStudentsByProject(int idProject);
        public bool AddStudent(StudentDTO newStudent);
        public bool EditStudent(StudentDTO editStudent);
    }
}