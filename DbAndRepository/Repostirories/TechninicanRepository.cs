using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DbAndRepository.Repostirories
{
    public class TechninicanRepository : GenericsRepository<Technician>, ITechnicianRepository
    {
        private Technician tc;
        static Random rnd=new Random();
        public TechninicanRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            tc = GetById(id);
            database.Set<Technician>().Remove(tc);
            database.Entry<Technician>(tc).State = EntityState.Deleted;
            database.SaveChanges();
        }
        public override Technician GetById(int id)
        {
            return Get(i => i.ID == id).FirstOrDefault();
        }

        public List<TechWorks> GetWorksByTechnician(int id)
        {
            throw new NotImplementedException();
        }

        public int GetAvailableTechnicianCount()
        {
            return GetAll().Count(x => x.Available == "Available");
        }

        public override void Update(Technician entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }

        public Technician GetAvailableTechnician()
        {
           var res=Get(x => x.Available == "Available").FirstOrDefault() ??
                   Get(x => x.ID == rnd.Next(GetAll().Count())).FirstOrDefault();
            return res;
        }
    }
}
