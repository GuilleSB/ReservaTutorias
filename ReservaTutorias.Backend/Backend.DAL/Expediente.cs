using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Expediente : ICRUD<Data.Expediente>
    {
        private Repository<Data.Expediente> _repo = null;

        public Expediente(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Expediente>(dbContext);
        }

        public void Delete(Data.Expediente t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Expediente> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Expediente GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Expediente t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Expediente t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
