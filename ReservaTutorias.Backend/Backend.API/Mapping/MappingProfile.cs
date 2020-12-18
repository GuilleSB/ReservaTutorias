using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.API.Mapping
{
    // Implementamos el casteo de objetos del automapper para no tener referencias circulares
    public class MappingProfile : Profile
    {
        // Necesitamos crear este metodo para poder direccionar el casteo de los objetos
        public MappingProfile()
        {
            CreateMap<DO.Objects.Materia, DataModels.Materia>();
            CreateMap<DO.Objects.Tema, DataModels.Tema>();

            CreateMap<DataModels.Materia, DO.Objects.Materia>();
            CreateMap<DataModels.Tema, DO.Objects.Tema>();

            
        }

    }
}
