using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils;
using ReservaTutorias.Frontend.Utils.Extensions;
using ReservaTutorias.Frontend.Utils.Filters;

namespace ReservaTutorias.Frontend.Controllers
{
    [AuthorizeView("Administrador")]
    public class UsuarioController : Controller
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        private readonly ISeguridad _seguridad;
        public UsuarioController(ISeguridad iseguridad)
        {
            _seguridad = iseguridad;
        }
        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            List<Usuario> aux = new List<Usuario>();
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Usuario");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Usuario>>(auxres);
                    aux = aux.FindAll(x => x.IdUsuario != sesion.IdUsuario);
                }
            }
            return View(aux);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Usuario = await GetUsuarioById(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            return View(Usuario);
        }

        // GET: Usuario/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                // Enctriptar contraseña
                Usuario.Clave = _seguridad.CreatePwdHash(Usuario.Clave);
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(Usuario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Usuario", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Error al crear el usuario");
            return View(Usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Usuario = await GetUsuarioById(id);
            if (Usuario == null)
            {
                return NotFound();
            }
            return View(Usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                Usuario.Clave = GetUsuarioById(Usuario.IdUsuario).Result.Clave;
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(Usuario);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Usuario/" + Usuario.IdUsuario, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var auxUsuario = await GetUsuarioById(id);
                    if (auxUsuario == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Usuario = await GetUsuarioById(id);
            if (Usuario == null)
            {
                return NotFound();
            }
            return View(Usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/Usuario/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            var Usuario = await GetUsuarioById(id);
            ModelState.AddModelError(string.Empty, "No se puede eliminar el usuario, pues tiene registros dependientes");
            return View(Usuario);
        }



        private async Task<Usuario> GetUsuarioById(int? id)
        {
            Usuario aux = new Usuario();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Usuario/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<Usuario>(auxres);
                }
            }
            return aux;
        }
    }
}
