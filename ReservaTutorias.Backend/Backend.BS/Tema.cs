﻿using Backend.DAL.EF;
using Backend.DO.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data = Backend.DO.Objects;

namespace Backend.BS
{
    public class Tema : ICRUD<Data.Tema>
    {
        private readonly BackendDbContext _repo = null;
        public Tema(BackendDbContext context)
        {
            _repo = context;
        }

        public void Delete(Data.Tema t)
        {
            new DAL.Tema(_repo).Delete(t);
        }

        public IEnumerable<Data.Tema> GetAll()
        {
            return new DAL.Tema(_repo).GetAll();
        }
        public async Task<IEnumerable<Data.Tema>> GetAllInclude()
        {
            return await new DAL.Tema(_repo).GetAllInclude();
        }
        public async Task<Data.Tema> GetByIdInclude(int id)
        {
            return await new DAL.Tema(_repo).GetByIdInclude(id);
        }
        public Data.Tema GetOneById(int id)
        {
            return new DAL.Tema(_repo).GetOneById(id);
        }

        public void Insert(Data.Tema t)
        {
            new DAL.Tema(_repo).Insert(t);
        }

        public void Update(Data.Tema t)
        {
            new DAL.Tema(_repo).Update(t);
        }
    }
}
