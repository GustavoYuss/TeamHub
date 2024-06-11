using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;

namespace TeamsHubWebClient.Gateways.Providers
{
    public class FileManagerRestProvider : IFileManager
    {

        private HttpClient ClientServiceFile;
        private ILogger<FileManagerRestProvider> _logger;

        public FileManagerRestProvider(ILogger<FileManagerRestProvider> logger, IHttpClientFactory httpClientFactory)
        {
            ClientServiceFile = httpClientFactory.CreateClient("ApiGateWay");
            _logger = logger;
        }

        public int DeleteFile(int IdDocument)
        {
            throw new NotImplementedException();
        }

        public List<DocumentDTO>? GetFilesByProjectt(int idProject)
        {
            try
            {
                var result = ClientServiceFile.GetAsync($"/TeamHub/File/{idProject}").Result;
                result.EnsureSuccessStatusCode();
                var response = result.Content.ReadFromJsonAsync<List<DocumentDTO>>().Result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }
}