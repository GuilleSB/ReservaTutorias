using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Horario : ICRUD<Data.Horario>
    {
        private Repository<Data.Horario> _repo = null;

        public Horario(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Horario>(dbContext);
        }

        public void Delete(Data.Horario t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Horario> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Horario GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Horario t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Horario t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
