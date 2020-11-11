using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Horario : ICRUD<Data.Horario>
    {
        private readonly BackendDbContext _repo = null;
        public Horario(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Horario t)
        {
            new DAL.Horario(_repo).Delete(t);
        }

        public IEnumerable<Data.Horario> GetAll()
        {
            return new DAL.Horario(_repo).GetAll();
        }

        public Data.Horario GetOneById(int id)
        {
            return new DAL.Horario(_repo).GetOneById(id);
        }

        public void Insert(Data.Horario t)
        {
            new DAL.Horario(_repo).Insert(t);
        }

        public void Update(Data.Horario t)
        {
            new DAL.Horario(_repo).Update(t);
        }
    }
}
