namespace DAL.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}
