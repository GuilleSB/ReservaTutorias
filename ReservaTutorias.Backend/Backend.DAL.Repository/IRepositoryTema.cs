using Backend.DO.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository
{
    public interface IRepositoryTema: IRepository<Tema>
    {
        Task<IEnumerable<Tema>> GetAllWithTemaAsync();
        Task<Tema> GetWithTemaByIdAsync(int id);
    }
}
