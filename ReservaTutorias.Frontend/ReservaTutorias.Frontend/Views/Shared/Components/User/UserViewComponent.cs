using Microsoft.AspNetCore.Mvc;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Views.Shared.Components.User
{
    public class UserViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var session = HttpContext.Session.GetObject<Usuario>("session");
            ViewBag.NombreUsuario = session.Nombre + " " + session.Apellidos;
            return View("UserLogged");
        }
    }
}
