using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Backend.DO.Objects
{
    public partial class Materia
    {
        public Materia()
        {
            Tema = new HashSet<Tema>();
        }

        [Key]
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }

        public virtual ICollection<Tema> Tema { get; set; }
    }
}
