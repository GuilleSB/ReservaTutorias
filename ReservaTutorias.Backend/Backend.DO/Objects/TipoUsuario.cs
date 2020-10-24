using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class TipoUsuario
    {
        [Key]
        public int IdTipoUsuario { get; set; }
        public string Tipo { get; set; }
    }
}
