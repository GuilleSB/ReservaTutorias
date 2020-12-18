using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Tema = new HashSet<Tema>();
        }
        [Key]
        [DisplayName("Id")]
        public int IdMateria { get; set; }
        [DisplayName("Nombre")]
        public string NombreMateria { get; set; }

        public virtual ICollection<Tema> Tema { get; set; }
    }
}
