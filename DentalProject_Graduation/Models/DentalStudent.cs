using System.ComponentModel.DataAnnotations.Schema;

namespace DentalProject_Graduation.Models
{
    public class DentalStudent
    {

        public int DentalStudentId { get; set; }
        public string CollegeLevel { get; set; }

       
        public  ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { set; get; }
        public  ICollection<CaseInformation> CaseInformations { get; set; }
        ////
        //public DentalStudent()
        //{
        // CaseInformations = new List<CaseInformation>();
        //}
    }
}
