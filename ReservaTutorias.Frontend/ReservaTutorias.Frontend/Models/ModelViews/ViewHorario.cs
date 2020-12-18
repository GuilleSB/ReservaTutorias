using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Models.ModelViews
{
    public class ViewHorario
    {
        public Horario Horario { get; set; }
        public Usuario Tutor { get; set; }
        public Tema Tema { get; set; }
    }
}
