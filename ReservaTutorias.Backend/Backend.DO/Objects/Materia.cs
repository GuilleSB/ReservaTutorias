using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
    }
}
