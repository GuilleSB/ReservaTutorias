using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Backend.API.DataModels
{
    public partial class Tema
    {
        [Key]
        public int IdTema { get; set; }
        public string NombreTema { get; set; }
        public int IdMateria { get; set; }
        public Materia Materia {get;set;}

    }
}
