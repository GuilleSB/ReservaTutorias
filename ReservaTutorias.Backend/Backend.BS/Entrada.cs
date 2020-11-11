using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Entrada : ICRUD<Data.Entrada>
    {
        private readonly BackendDbContext _repo = null;
        public Entrada(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Entrada t)
        {
            new DAL.Entrada(_repo).Delete(t);
        }

        public IEnumerable<Data.Entrada> GetAll()
        {
            return new DAL.Entrada(_repo).GetAll();
        }

        public Data.Entrada GetOneById(int id)
        {
            return new DAL.Entrada(_repo).GetOneById(id);
        }

        public void Insert(Data.Entrada t)
        {
            new DAL.Entrada(_repo).Insert(t);
        }

        public void Update(Data.Entrada t)
        {
            new DAL.Entrada(_repo).Update(t);
        }
    }
}
