using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class TipoUsuario : ICRUD<Data.TipoUsuario>
    {
        private readonly BackendDbContext _repo = null;
        public TipoUsuario(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.TipoUsuario t)
        {
            new DAL.TipoUsuario(_repo).Delete(t);
        }

        public IEnumerable<Data.TipoUsuario> GetAll()
        {
            return new DAL.TipoUsuario(_repo).GetAll();
        }

        public Data.TipoUsuario GetOneById(int id)
        {
            return new DAL.TipoUsuario(_repo).GetOneById(id);
        }

        public void Insert(Data.TipoUsuario t)
        {
            new DAL.TipoUsuario(_repo).Insert(t);
        }

        public void Update(Data.TipoUsuario t)
        {
            new DAL.TipoUsuario(_repo).Update(t);
        }
    }
}
