using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        public int IdTutor { get; set; }
        public int IdMateria { get; set; }
        public int IdTema { get; set; }
        public DateTime FechaHora { get; set; }
        public int LimiteEstudiantes { get; set; }
    }
}
