using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Materia : ICRUD<Data.Materia>
    {
        private readonly BackendDbContext _repo = null;
        public Materia(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Materia t)
        {
            new DAL.Materia(_repo).Delete(t);
        }

        public IEnumerable<Data.Materia> GetAll()
        {
            return new DAL.Materia(_repo).GetAll();
        }

        public Data.Materia GetOneById(int id)
        {
            return new DAL.Materia(_repo).GetOneById(id);
        }

        public void Insert(Data.Materia t)
        {
            new DAL.Materia(_repo).Insert(t);
        }

        public void Update(Data.Materia t)
        {
            new DAL.Materia(_repo).Update(t);
        }
    }
}
