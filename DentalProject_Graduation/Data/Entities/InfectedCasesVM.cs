using DentalProject_Graduation.Models;
namespace DentalProject_Graduation.Data.Entities

{
	public class InfectedCasesVM
	{

		//props of user
		public string?  Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? phone { set;get; }
		//props patient
		public string ?Age { get; set; }

		//props case
		public byte[]? DiseasePicture { get; set; }
		public int? DiseaseId { set; get; }
		public string DiseaseName { get; set; }	

		//props caseinformation

		public int? CaseInformationID { set; get; }

		public byte[]? photoAfter { set; get; }
		public DateTime? Appoinment { set; get; }




	}
}
