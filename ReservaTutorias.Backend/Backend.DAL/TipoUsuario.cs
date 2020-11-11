using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class TipoUsuario : ICRUD<Data.TipoUsuario>
    {
        private Repository<Data.TipoUsuario> _repo = null;

        public TipoUsuario(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.TipoUsuario>(dbContext);
        }

        public void Delete(Data.TipoUsuario t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.TipoUsuario> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.TipoUsuario GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.TipoUsuario t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.TipoUsuario t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
