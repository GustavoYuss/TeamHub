namespace TeamHubServiceUser.Entities;

public partial class StudentDTO
{
    public int IdStudent { get; set; }

    public string Name { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public string SurName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}