using TeamsHubWebClient.DTOs;
using System;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IFileManager
    {
        public Task<List<DocumentDTO>>? GetFilesByProject(int idProject);
        public Task DeleteFile(int IdDocument);
        public Task AddFile(IFormFile file, int idProject);
    }
}