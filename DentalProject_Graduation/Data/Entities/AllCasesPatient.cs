namespace DentalProject_Graduation.Data.Entities
{
    public class AllCasesPatient
    {
        public int CaseId { set; get; }

        public int DiseaseId { set;get ;}

        public string? DiseaseName { set; get; }

        public byte[] ?DiseasePicture { get; set; }
   
    }
}
