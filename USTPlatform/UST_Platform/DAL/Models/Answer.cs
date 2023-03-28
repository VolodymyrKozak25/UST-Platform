namespace DAL.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public int QuestionId { get; set; }

    public string AnswerText { get; set; } = string.Empty;

    public bool? IsCorrect { get; set; }

    public virtual Question Question { get; set; } = new Question();

    public virtual ICollection<Studentresult> Studentresults { get; } = new List<Studentresult>();
}
