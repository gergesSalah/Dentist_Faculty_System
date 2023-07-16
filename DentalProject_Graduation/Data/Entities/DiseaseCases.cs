namespace DentalProject_Graduation.Data.Entities
{
    public class DiseaseCases
    {
        public int CaseId { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public byte[] photo { get; set; } 

        public DateTime ApplyOn { set; get; }
    }
}
