using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Reserva : ICRUD<Data.Reserva>
    {
        private Repository<Data.Reserva> _repo = null;

        public Reserva(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Reserva>(dbContext);
        }

        public void Delete(Data.Reserva t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Reserva> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Reserva GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Reserva t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Reserva t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
