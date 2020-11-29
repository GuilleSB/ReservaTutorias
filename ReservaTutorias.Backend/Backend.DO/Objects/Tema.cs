using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Backend.DO.Objects
{
    public partial class Tema
    {
        [Key]
        public int IdTema { get; set; }
        public string NombreTema { get; set; }
    }
}
