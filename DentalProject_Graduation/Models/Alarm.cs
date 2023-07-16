


using System.ComponentModel.DataAnnotations.Schema;

namespace DentalProject_Graduation.Models
{
    public class Alarm
    {
        [ForeignKey("dentalStudent")]
        public int IdDentail { get; set; }

        [ForeignKey("diseasee")]
        public int IdDiseaase { get; set; }
        public DateTime ApplyOn { get; set; }

        public Disease diseasee { get; set; }
        public DentalStudent dentalStudent { get; set; }    
    }
}
