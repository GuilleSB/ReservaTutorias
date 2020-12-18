using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Horario
    {
        [Key]
        [DisplayName("Horario")]
        public int IdHorario { get; set; }
        [DisplayName("Tutor")]
        public int IdTutor { get; set; }
        [DisplayName("Tema")]
        public int IdTema { get; set; }
        [DisplayName("Fecha y hora")]
        public DateTime FechaHora { get; set; }
        [DisplayName("Máximo de estudiantes")]
        public int LimiteEstudiantes { get; set; }
        [DisplayName("Link de la clase")]
        public string LinkReunion { get; set; }
    }
}
