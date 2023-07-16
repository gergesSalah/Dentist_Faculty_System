using System.ComponentModel.DataAnnotations;

namespace DentalProject_Graduation.Models
{
    public class Question
    {
        [Key]
        public string Question_ID { get; set; }
        public string QuestionName { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
