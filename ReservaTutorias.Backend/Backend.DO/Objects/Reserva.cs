using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Backend.DO.Objects
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdHorario { get; set; }
        public int IdMateria { get; set; }
        public int IdTema { get; set; }
        public int IdEstudiante { get; set; }
    }
}
