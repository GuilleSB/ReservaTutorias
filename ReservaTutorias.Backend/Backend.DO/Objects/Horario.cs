using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        public int IdTutor { get; set; }
        public string Dia { get; set; }
        public DateTime Hora { get; set; }
    }
}
