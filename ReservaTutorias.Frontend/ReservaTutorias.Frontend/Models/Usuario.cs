using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ReservaTutorias.Frontend.Models
{
    public partial class Usuario
    {
        [Key]
        [DisplayName("Id")]
        public int IdUsuario { get; set; }
        [DisplayName("Cédula")]
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        [DisplayName("Tipo de usuario")]
        public string TipoUsuario { get; set; }
    }
}
