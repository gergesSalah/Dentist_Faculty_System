using Microsoft.CodeAnalysis.Options;

namespace DentalProject_Graduation.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
