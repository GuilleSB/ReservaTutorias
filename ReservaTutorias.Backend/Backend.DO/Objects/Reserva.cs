using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Backend.DO.Objects
{
    public class Reserva
    {
        [Key]
        [Column(Order =1)]
        public int IdHorario { get; set; }
        [Key]
        [Column(Order = 2)]
        public int IdMateria { get; set; }
        public int IdTema { get; set; }
        public int IdEstudiante { get; set; }
    }
}
