namespace DAL.Models;

public partial class Studentresult
{
    public string StudentId { get; set; } = string.Empty;

    public int TestId { get; set; }

    public int AnswerId { get; set; }

    public virtual Answer Answer { get; set; } = new Answer();

    public virtual User Student { get; set; } = new User();

    public virtual Test Test { get; set; } = new Test();
}
