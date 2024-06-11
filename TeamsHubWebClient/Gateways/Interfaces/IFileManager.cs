using TeamsHubWebClient.DTOs;

namespace TeamsHubWebClient.Gateways.Interfaces
{
    public interface IFileManager
    {
        public List<DocumentDTO>? GetFilesByProjectt(int idProject);

        public int DeleteFile(int IdDocument);
    }
}