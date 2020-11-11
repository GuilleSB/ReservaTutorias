using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Expediente : ICRUD<Data.Expediente>
    {
        private readonly BackendDbContext _repo = null;
        public Expediente(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Expediente t)
        {
            new DAL.Expediente(_repo).Delete(t);
        }

        public IEnumerable<Data.Expediente> GetAll()
        {
            return new DAL.Expediente(_repo).GetAll();
        }

        public Data.Expediente GetOneById(int id)
        {
            return new DAL.Expediente(_repo).GetOneById(id);
        }

        public void Insert(Data.Expediente t)
        {
            new DAL.Expediente(_repo).Insert(t);
        }

        public void Update(Data.Expediente t)
        {
            new DAL.Expediente(_repo).Update(t);
        }
    }
}
