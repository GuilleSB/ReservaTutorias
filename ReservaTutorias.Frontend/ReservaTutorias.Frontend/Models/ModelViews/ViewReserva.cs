using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Models.ModelViews
{
    public class ViewReserva
    {
        public Reserva Reserva { get; set; }
        public ViewHorario Horario { get; set; }
    }
}
