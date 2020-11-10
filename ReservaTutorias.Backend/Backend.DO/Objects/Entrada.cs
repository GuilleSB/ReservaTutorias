using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Entrada
    {
        [Key]
        public int IdEntrada { get; set; }
        public int IdExpediente { get; set; }
        public DateTime FechaRegistra { get; set; }
        public string ComentarioEntrada { get; set; }
    }
}
