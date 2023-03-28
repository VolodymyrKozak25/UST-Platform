namespace DAL.Models;

public partial class Studentresult
{
    public int StudentId { get; set; }

    public int TestId { get; set; }

    public int AnswerId { get; set; }

    public virtual Answer Answer { get; set; } = new Answer();

    public virtual Student Student { get; set; } = new Student();

    public virtual Test Test { get; set; } = new Test();
}
