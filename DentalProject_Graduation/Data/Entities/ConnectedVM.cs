namespace DentalProject_Graduation.Data.Entities
{
    public class ConnectedVM
    {
        public string Disease { set; get; }
        public string fname { set; get; }
        public string lname { set; get; }
        public int DentailId { set; get; }
        public int CaseId { set; get; }
        public string Message { set; get; }
        public string MailPatient { set; get; }
        public DateTime Appointment { set; get; }
    }
}
