using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Tema
    {
        [Key]
        [DisplayName("Id")]
        public int IdTema { get; set; }
        [DisplayName("Nombre")]
        public string NombreTema { get; set; }
        [DisplayName("Materia")]
        public int IdMateria { get; set; }
        public virtual Materia Materia { get; set; }
    }
}
