using TeamsHubWebClient.DTOs;
using TeamsHubWebClient.Gateways.Interfaces;
using Grpc.Core;
using FilePackage;
using Grpc.Net.Client;

namespace TeamsHubWebClient.Gateways.Providers
{
    public class FileManagerRestProvider : IFileManager
    {

        private HttpClient ClientServiceFile;
        private FileManagement.FileManagementClient client;
        private ILogger<FileManagerRestProvider> _logger;

        public FileManagerRestProvider(ILogger<FileManagerRestProvider> logger, IHttpClientFactory httpClientFactory)
        {
            ClientServiceFile = httpClientFactory.CreateClient("ApiGateWay");
            _logger = logger;
        }

        public async Task DeleteFile(int IdDocument)
        {
            var channelOptions = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure
            };

            using var channel = GrpcChannel.ForAddress("http://172.16.0.6:8080", channelOptions);
            client = new FileManagement.FileManagementClient(channel);
            var reply = await client.DeleteFileAsync(new DeleteRequest { IdFile = IdDocument });
        }

        public async Task AddFile(IFormFile file, int idProject)
        {
            if (file == null || file.Length == 0)
            {   
                Console.WriteLine("SUP");
            }
            else
            {
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var channelOptions = new GrpcChannelOptions
                {
                    Credentials = ChannelCredentials.Insecure
                };

                using var channel = GrpcChannel.ForAddress("http://172.16.0.6:8080", channelOptions);
                client = new FileManagement.FileManagementClient(channel);
                var reply = await client.SaveFileAsync(new FileRequest
                {
                    ProjectName = idProject,
                    FileName = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    FileString = Google.Protobuf.ByteString.CopyFrom(fileBytes)
                });
            }
        }

        public async Task<List<DocumentDTO>>? GetFilesByProject(int idProject)
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