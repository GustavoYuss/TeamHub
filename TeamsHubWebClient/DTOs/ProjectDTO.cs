
namespace TeamsHubWebClient.DTOs;

public partial class ProjectDTO
{
    public int IdProject { get; set; }

    public string? Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int IdStudent { get; set; }
    public string Status { get; set; }

    public string GetStartDate
    {
        get
        {
            return StartDate?.ToString("dd/MMMM/yyyy");
        }
    }

    public string GetEndDate
    {
        get
        {
            return EndDate?.ToString("dd/MMMM/yyyy");
        }
    }
}