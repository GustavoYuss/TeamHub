
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
        try {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(sessionLoginRequest.Password);
            byte[] hash = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string passwordMD5 = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            sessionLoginRequest.Password = passwordMD5;

            var resultado = clientUserIdentityService.PostAsJsonAsync<SessionLoginRequest> ($"/TeamHub/Sessions/validateUser", sessionLoginRequest).Result;
            resultado.EnsureSuccessStatusCode();
            var respuesta = resultado.Content.ReadFromJsonAsync<UserValidationResponse>().Result;    
            return respuesta;
        } catch (Exception e) {
            return new UserValidationResponse() { IsValid=false };
        }
    }
}