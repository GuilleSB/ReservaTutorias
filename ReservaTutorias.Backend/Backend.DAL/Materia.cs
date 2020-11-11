using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Materia : ICRUD<Data.Materia>
    {
        private Repository<Data.Materia> _repo = null;

        public Materia(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Materia>(dbContext);
        }

        public void Delete(Data.Materia t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Materia> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Materia GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Materia t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Materia t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
