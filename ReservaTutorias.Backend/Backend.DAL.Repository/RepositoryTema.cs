using Backend.DAL.EF;
using Backend.DO.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository
{
    public class RepositoryTema : Repository<Tema>, IRepositoryTema
    {
        public RepositoryTema(BackendDbContext context)
            : base(context)
        { }
        public async Task<IEnumerable<Tema>> GetAllWithTemaAsync()
        {
            return await BackendDbContext.Tema
                .Include(m => m.Materia)
                .ToListAsync();
        }

        public async Task<Tema> GetWithTemaByIdAsync(int id)
        {
            return await BackendDbContext.Tema
                .Include(m => m.Materia)
                .SingleOrDefaultAsync(m => m.IdMateria == id);
        }

        private BackendDbContext BackendDbContext
        {
            get { return _dbContext; }
        }
    }
}
