using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using FilePackage;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeamsHubWebClient.Gateways.Interfaces;
using TeamsHubWebClient.SinglentonClasses;
using TeamsHubWebClient.DTOs;
using Grpc.Core;

namespace TeamsHubWebClient.Pages
{
    public class FileModule : PageModel
    {
        private readonly ILogger<FileModule> _logger;

        private readonly IFileManager _FileManager;

        public List<DocumentDTO>? DocumenttList;
        public int idProject = ProjectSinglenton.Id;
        
        public FileModule(ILogger<FileModule> logger, IFileManager fileManager)
        {
            _logger = logger;
            _FileManager = fileManager;
        }  

        public async void OnGet()
        {
            DocumenttList = await _FileManager.GetFilesByProject(idProject);
        }

        public async Task OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return; // O devuelve algún error, según tu lógica.
            }

            // Convertir el archivo a bytes
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

            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:5001", channelOptions);
                var client = new FileManagement.FileManagementClient(channel);
                var reply = await client.SaveFileAsync(new FileRequest
                {
                    ProjectName = idProject,
                    FileName = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    FileString = Google.Protobuf.ByteString.CopyFrom(fileBytes)
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}