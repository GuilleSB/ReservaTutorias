using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Utils
{
    public interface IEnviaCorreo
    {
        public bool EnviarCorreo(string destinatario,string tema, string mensaje);
    }
}
