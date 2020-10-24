using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Tema
    {
        [Key]
        public int IdTema { get; set; }
        public string NombreTema { get; set; }
    }
}
