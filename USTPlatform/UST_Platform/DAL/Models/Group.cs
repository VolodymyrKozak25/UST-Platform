namespace DAL.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
