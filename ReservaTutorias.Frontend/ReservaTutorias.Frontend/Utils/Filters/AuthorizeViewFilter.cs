using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Utils.Filters
{
    public class AuthorizeViewFilter :IAuthorizationFilter
    {
        private readonly string _modulo;
        private readonly string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        public AuthorizeViewFilter(string modulo)
        {
            _modulo = modulo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var session = context.HttpContext.Session.GetObject<Usuario>("session");
                if(session == null)
                    context.Result = new RedirectToActionResult("Error404", "Error", new { });
                else
                {
                    var perfiles = _modulo.Split(",");
                    var validaRol = perfiles.ToList().Any(x => x == session.TipoUsuario);
                    if (!validaRol)
                        context.Result = new RedirectToActionResult("Error404", "Error", new { });
                }
                
            }
            catch (Exception)
            {
                context.Result = new RedirectToActionResult("Error404", "Error", new { });
            }
        }

    }
}
