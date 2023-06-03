namespace DAL.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string CourseKey { get; set; } = string.Empty;

    public int MaxTeachers { get; set; }

    public virtual ICollection<Test> Tests { get; } = new List<Test>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
