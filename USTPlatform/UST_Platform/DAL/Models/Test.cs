namespace DAL.Models;

public partial class Test
{
    public int TestId { get; set; }

    public int CourseId { get; set; }

    public string Name { get; set; } = string.Empty;

    public TimeSpan TimeLimit { get; set; }

    public DateTime FinalDate { get; set; }

    public virtual Course Course { get; set; } = new Course();

    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    public virtual ICollection<Studentresult> Studentresults { get; } = new List<Studentresult>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
