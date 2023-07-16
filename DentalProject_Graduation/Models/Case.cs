using DentalProject_Graduation.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalProject_Graduation.Models
{
    public  class Case
    {
        public int CaseId { get; set; }
        public byte[] DiseasePicture { get; set; }
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual Disease Disease { get; set; }
        public virtual CaseInformation CaseInformation { get; set; }

        public byte Viald { set; get; }

       
    }

    
}
