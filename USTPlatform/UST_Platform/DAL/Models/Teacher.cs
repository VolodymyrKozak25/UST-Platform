namespace DAL.Models;

public partial class Teacher
{
    public int UserId { get; set; }

    public virtual User User { get; set; } = new User();

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
