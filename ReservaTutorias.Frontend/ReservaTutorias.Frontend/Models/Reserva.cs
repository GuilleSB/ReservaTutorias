using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        public int IdEstudiante { get; set; }
        public int IdHorario { get; set; }

    }
}
