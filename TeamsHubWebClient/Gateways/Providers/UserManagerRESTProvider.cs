using System.Text;
using TeamHubServiceUser.Entities;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Gateways.Providers
{
    public class UserManagerRESTProvider : IUserManager
    {

        HttpClient clientServiceUser;
        ILogger<UserManagerRESTProvider> _logger;


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
                var response = result.Content.ReadFromJsonAsync<Boolean>().Result;
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EditStudent(StudentDTO editStudent)
        {
            throw new NotImplementedException();
        }

        public List<User> getStudentsByProject(int idProject)
        {
            List<User> response = null;
            try
            {
                var result = clientServiceUser.GetAsync($"/TeamHub/Users/ByProject/{1}").Result;
                result.EnsureSuccessStatusCode();
                response = result.Content.ReadFromJsonAsync<List<User>>().Result;
            }
            catch (System.Exception)
            {

                throw;
            }
            return response;
        }
    }
}
