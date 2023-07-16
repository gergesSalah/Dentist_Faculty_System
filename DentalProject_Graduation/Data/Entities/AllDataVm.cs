using DentalProject_Graduation.Models;

namespace DentalProject_Graduation.Data.Entities
{
    public class AllDataVm
    {
        public string? MailPatient { set; get; }
       
        public string Dname { get; set; }
        public string DoseaseNAme { get; set; }
        public string DiseaseDescription { get; set; }
        public string age { get; set; }

        public string fname { get; set; }
        public string lname { get; set; }

        public int CaseId { set; get; }
        public int CaseinfoId { set; get; }
        public byte[] DiseasePicture { get; set; }
        public string? Message { set; get; }
        public DateTime Appointment { set; get; }



    }
}
