using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Backend.DO.Objects
{
    public partial class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdEstudiante { get; set; }
        public int IdHorario { get; set; }
        public string Notas { get; set; }
    }
}
