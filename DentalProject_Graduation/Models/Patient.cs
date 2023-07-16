using System.ComponentModel.DataAnnotations.Schema;

namespace DentalProject_Graduation.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Age { get; set; }

        [ForeignKey("user")]
 
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }

        public virtual ICollection<Case>? Cases { get; set; }
    }
}
