
using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces{

    public interface IUserIdentityManager
    {
        public UserValidationResponse ValidateUser(SessionLoginRequest sessionLoginRequest);
    }
}