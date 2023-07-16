namespace DentalProject_Graduation.Models
{
    public class Disease
    {
        public   int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseDescription { get; set; }
        public virtual IEnumerable<Case> Cases{ get; set; }
    }
}
