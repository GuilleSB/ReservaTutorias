using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Tema : ICRUD<Data.Tema>
    {
        private Repository<Data.Tema> _repo = null;

        public Tema(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Tema>(dbContext);
        }

        public void Delete(Data.Tema t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Tema> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Tema GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Tema t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Tema t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
