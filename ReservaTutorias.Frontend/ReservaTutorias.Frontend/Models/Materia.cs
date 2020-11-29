using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }

    }
}
