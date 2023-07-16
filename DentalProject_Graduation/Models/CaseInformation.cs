using FluentNHibernate.Conventions.Inspections;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalProject_Graduation.Models
{
    public class CaseInformation
    {
        [Key]
      public int CaseInformationId { set; get; }
        public DateTime Appointment { get; set; }
        public byte[]? PhotoAfter { get; set; }
        [ForeignKey("DentalStudent")] 
        public int DentalStudentId { get; set; }
        public virtual Case Case { get; set; }
        public int CaseId { get; set; }
        public virtual DentalStudent DentalStudent { get; set; }


    }

}
