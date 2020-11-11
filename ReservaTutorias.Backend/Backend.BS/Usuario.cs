using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Usuario : ICRUD<Data.Usuario>
    {
        private readonly BackendDbContext _repo = null;
        public Usuario(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Usuario t)
        {
            new DAL.Usuario(_repo).Delete(t);
        }

        public IEnumerable<Data.Usuario> GetAll()
        {
            return new DAL.Usuario(_repo).GetAll();
        }

        public Data.Usuario GetOneById(int id)
        {
            return new DAL.Usuario(_repo).GetOneById(id);
        }

        public void Insert(Data.Usuario t)
        {
            new DAL.Usuario(_repo).Insert(t);
        }

        public void Update(Data.Usuario t)
        {
            new DAL.Usuario(_repo).Update(t);
        }
    }
}
