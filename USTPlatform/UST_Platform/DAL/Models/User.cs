namespace DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string UserType { get; set; } = string.Empty;

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
