namespace DentalProject_Graduation.Data.Entities
{
    public class InfectedCasePatientVM
    {

         public int PatientId { get; set; }
        public int CaseId { set; get; }
         public string? DiseaseName { get; set; }
         public string? dentalNameF { get; set; }
        public string? dentalNameL { get; set; }
        public string? Phone { get; set; }
        public DateTime? Appointment { get; set; }
         
        public byte[]? DiseasePhoto { set; get; }
        public byte[]? PhotoAfter { set; get; }








    }
}
