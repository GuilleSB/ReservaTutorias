using Backend.DAL.EF;
using Backend.DAL.Repository;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using Data = Backend.DO.Objects;

namespace Backend.DAL
{
    public class Usuario : ICRUD<Data.Usuario>
    {
        private Repository<Data.Usuario> _repo = null;

        public Usuario(BackendDbContext dbContext)
        {
            _repo = new Repository<Data.Usuario>(dbContext);
        }

        public void Delete(Data.Usuario t)
        {
            _repo.Delete(t);
            _repo.Commit();
        }

        public IEnumerable<Data.Usuario> GetAll()
        {
            return _repo.GetAll();
        }

        public Data.Usuario GetOneById(int id)
        {
            return _repo.GetOneById(id);
        }

        public void Insert(Data.Usuario t)
        {
            _repo.Insert(t);
            _repo.Commit();
        }

        public void Update(Data.Usuario t)
        {
            _repo.Update(t);
            _repo.Commit();
        }
    }
}
