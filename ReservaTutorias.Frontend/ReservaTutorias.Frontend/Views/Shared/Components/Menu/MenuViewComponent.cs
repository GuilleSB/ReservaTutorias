using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReservaTutorias.Frontend.Views.Shared.Components.Menu
{
    public class MenuViewComponent: ViewComponent
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        public IViewComponentResult Invoke()
        {
            var session = HttpContext.Session.GetObject<Usuario>("session");
            var perfilUsuario = session.TipoUsuario;
            ViewBag.Perfil = perfilUsuario;
            return View("MenuPrincipal");
        }

        //private IList<string> GetPerfiles()
        //{
        //    List<Usuario> aux = new List<Usuario>();
        //    using (var cl = new HttpClient())
        //    {
        //        cl.BaseAddress = new Uri(baseurl);
        //        cl.DefaultRequestHeaders.Clear();
        //        cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage res = cl.GetAsync("api/Usuario").Result;

        //        if (res.IsSuccessStatusCode)
        //        {
        //            var auxres = res.Content.ReadAsStringAsync().Result;
        //            aux = JsonConvert.DeserializeObject<List<Usuario>>(auxres);
        //        }
        //    }
        //    return aux.Select(x => x.TipoUsuario).Distinct().ToList();
        //}
    }
}
