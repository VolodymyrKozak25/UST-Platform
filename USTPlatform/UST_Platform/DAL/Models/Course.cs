namespace DAL.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CourseKey { get; set; }

    public int? MaxTeachers { get; set; }

    public virtual ICollection<Test> Tests { get; } = new List<Test>();

    public virtual ICollection<Group> Groups { get; } = new List<Group>();

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
