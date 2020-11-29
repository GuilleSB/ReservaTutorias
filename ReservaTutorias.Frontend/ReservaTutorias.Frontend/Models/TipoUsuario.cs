using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class TipoUsuario
    {
        [Key]
        public int IdTipoUsuario { get; set; }
        public string Tipo { get; set; }
    }
}
