using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Reserva : ICRUD<Data.Reserva>
    {
        private readonly BackendDbContext _repo = null;
        public Reserva(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Reserva t)
        {
            new DAL.Reserva(_repo).Delete(t);
        }

        public IEnumerable<Data.Reserva> GetAll()
        {
            return new DAL.Reserva(_repo).GetAll();
        }

        public Data.Reserva GetOneById(int id)
        {
            return new DAL.Reserva(_repo).GetOneById(id);
        }

        public void Insert(Data.Reserva t)
        {
            new DAL.Reserva(_repo).Insert(t);
        }

        public void Update(Data.Reserva t)
        {
            new DAL.Reserva(_repo).Update(t);
        }
    }
}
