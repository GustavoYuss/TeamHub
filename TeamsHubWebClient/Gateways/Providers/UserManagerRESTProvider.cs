using System.Text;
using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Gateways.Providers
{
    public class UserManagerRESTProvider : IUserManager
    {
        private readonly HttpClient clientServiceUser;
        private readonly ILogger<UserManagerRESTProvider> _logger;

        public UserManagerRESTProvider(ILogger<UserManagerRESTProvider> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            clientServiceUser = httpClientFactory.CreateClient("ApiGateWay");
        }

        public bool AddStudent(StudentDTO newStudent)
        {
            try
            {
                var result = clientServiceUser.PostAsJsonAsync($"/TeamHub/Users", newStudent).Result;
                result.EnsureSuccessStatusCode();
                var response = result.Content.ReadFromJsonAsync<bool>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
                return false;
            }
        }

        public async Task<int> EditStudent(StudentDTO editStudent)
        {
            int result;
            String password = editStudent.Password;

            try
            {
                var content = JsonContent.Create(editStudent);
                var response = clientServiceUser.PutAsync("/TeamHub/Users/Edit", content).Result;
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadFromJsonAsync<Int16>();
            }
            catch (Exception ex)
            {
                result = -1;
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public List<User> GetStudentsByProject(int idProject)
        {
            try
            {
                var result = clientServiceUser.GetAsync($"/TeamHub/Users/ByProject/{idProject}").Result;
                result.EnsureSuccessStatusCode();
                var response = result.Content.ReadFromJsonAsync<List<User>>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students by project");
                throw;
            }
        }

        public List<User> SearchStudent(string student)
        {
            try
            {
                var result = clientServiceUser.GetAsync($"/TeamHub/Users/Search/{student}").Result;
                result.EnsureSuccessStatusCode();
                var responseDB = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Response:");
                Console.WriteLine(responseDB);
                var response = result.Content.ReadFromJsonAsync<List<User>>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching student");
                throw;
            }
        }

        public bool DeleteStudent(int idProject, int idStudent)
        {
            try
            {
                var result = clientServiceUser.DeleteAsync($"/TeamHub/Users/RemoveOfProject/{idProject}/{idStudent}").Result;
                result.EnsureSuccessStatusCode();
                var response = result.Content.ReadFromJsonAsync<bool>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student");
                return false;
            }
        }

        public bool AddStudentToProject(int idProject, int idStudent)
        {
            try
            {
                var result = clientServiceUser.PostAsync($"/TeamHub/Users/AddToProject/{idProject}/{idStudent}", null).Result;
                result.EnsureSuccessStatusCode();
                var response = result.Content.ReadFromJsonAsync<bool>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student to project");
                return false;
            }
        }

        public StudentDTO GetUserPersonalData(int studentID)
        {
            StudentDTO response;

            try
            {
                var result = clientServiceUser.GetAsync($"/TeamHub/Users/GetUserInformation/{studentID}").Result;
                result.EnsureSuccessStatusCode();
                response = result.Content.ReadFromJsonAsync<StudentDTO>().Result;
            }
            catch (Exception ex)
            {
                response = null;
                _logger.LogError(ex.Message);
            }

            return response;
        }


        public bool PasswordRecovery(string userEmail)
        {
            bool result;

            try
            {
                var response = clientServiceUser.GetAsync($"/TeamHub/Users/RecoveryPassword/{userEmail}").Result;
                response.EnsureSuccessStatusCode();
                result = true;
            }
            catch (System.Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
