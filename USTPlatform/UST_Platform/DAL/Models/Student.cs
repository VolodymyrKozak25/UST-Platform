namespace DAL.Models;

public partial class Student
{
    public int UserId { get; set; }

    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = new Group();

    public virtual ICollection<Studentresult> Studentresults { get; } = new List<Studentresult>();

    public virtual User User { get; set; } = new User();

    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}
