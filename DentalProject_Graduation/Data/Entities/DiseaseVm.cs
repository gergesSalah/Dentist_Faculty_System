using DentalProject_Graduation.Models;

namespace DentalProject_Graduation.Data.Entities
{
    public class DiseaseVm
    {
        private readonly ApplicationDbContext db;
        public DiseaseVm(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<Disease> GetAll()
        {
             return db.Diseases.ToList();   
        }
    }
}
