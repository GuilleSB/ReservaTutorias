using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Expediente
    {
        [Key]
        public int IdExpediente { get; set; }
        public int IdEstudiante { get; set; }
        public int IdTutor { get; set; }
        public string Observaciones { get; set; }
    }
}
