
using System.Security.Cryptography;
using System.Text;
using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

public class UserIdentityManagerRESTProvider : IUserIdentityManager
{
    HttpClient clientUserIdentityService;
    ILogger<UserIdentityManagerRESTProvider> iLogger;
    public UserIdentityManagerRESTProvider(IHttpClientFactory httpClientFactory)
    {
        clientUserIdentityService = httpClientFactory.CreateClient("ApiGateWay");
        this.iLogger = iLogger;
    }

    public UserValidationResponse ValidateUser(SessionLoginRequest sessionLoginRequest)
    {
        UserValidationResponse userValidationResponse;

        try {

            byte[] encodedPassword = new UTF8Encoding().GetBytes(sessionLoginRequest.Password);
            byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string passwordMD5 = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            var resultado = clientUserIdentityService.PostAsJsonAsync<SessionLoginRequest> ($"/TeamHub/Sessions/validateUser", sessionLoginRequest).Result;
            resultado.EnsureSuccessStatusCode();
            userValidationResponse = resultado.Content.ReadFromJsonAsync<UserValidationResponse>().Result; 
            
        } catch (Exception e) {

            userValidationResponse = null;

        }

        return userValidationResponse;
    }
}