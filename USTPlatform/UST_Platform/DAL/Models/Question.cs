namespace DAL.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TestId { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    public int Score { get; set; }

    public virtual ICollection<Answer> Answers { get; } = new List<Answer>();

    public virtual Test Test { get; set; } = new Test();
}
