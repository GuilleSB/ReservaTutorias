using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.DO.Objects
{
    public class Usuario
    {
		[Key]
		public int IdUsuario { get; set; }
		public string Cedula { get; set; }
		public string Nombre { get; set; }
		public string Apellidos { get; set; }
		public string Correo { get; set; }
		public string Clave { get; set; }
		public int IdTipoUsuario { get; set; }
	}
}
