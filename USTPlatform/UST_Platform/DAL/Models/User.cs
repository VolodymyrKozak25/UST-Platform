using Microsoft.AspNetCore.Identity;

namespace DAL.Models;

public partial class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public virtual ICollection<Studentresult>? Studentresults { get; set; }
}
