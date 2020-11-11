using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Entrada : ICRUD<Data.Entrada>
    {
        private Repository<Data.Entrada> _repo = null;

        public Entrada(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Entrada>(dbContext);
        }

        public void Delete(Data.Entrada t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Entrada> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Entrada GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Entrada t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Entrada t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}