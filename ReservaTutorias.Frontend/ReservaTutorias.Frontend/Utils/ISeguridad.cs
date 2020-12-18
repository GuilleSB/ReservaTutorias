using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Utils
{
    public interface ISeguridad
    {
        public string CreatePwdHash(string password);
        public string GenerarClave();
    }
}
