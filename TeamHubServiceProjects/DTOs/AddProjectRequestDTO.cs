

using TeamHubServiceProjects.Entities;

namespace TeamHubServiceProjects.DTOs
{
    public class AddProjectRequestDTO
    {
        public project ProjectNew { get; set; }
        public int StudentID { get; set; }
    }
}