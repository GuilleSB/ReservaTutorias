using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils;
using ReservaTutorias.Frontend.Utils.Extensions;
using ReservaTutorias.Frontend.Utils.Filters;

namespace ReservaTutorias.Frontend.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        private readonly ISeguridad _seguridad;
        private readonly IEnviaCorreo _enviaCorreo;
        public LoginController(ISeguridad seguridad,IEnviaCorreo enviaCorreo)
        {
            _seguridad = seguridad;
            _enviaCorreo = enviaCorreo;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind]Login login)
        {
            if(/*login.Clave == null ||*/ login.Correo == null)
            {
                ModelState.AddModelError(string.Empty, "Por favor, ingrese las credenciales");
                return View("Index");
            }
            // Enctriptar contraseña
            //login.Clave = _seguridad.CreatePwdHash(login.Clave);
            Usuario log = GetUsers()
                .Where(x => x.Correo == login.Correo /*&& x.Clave == login.Clave*/).FirstOrDefault();

            if (log != null)
            {
                HttpContext.Session.SetObject("session", log);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
                return View("Index");
            }
        }

        public ActionResult CerrarSesion()
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }
        }

        public ActionResult OlvidaClave()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Usuario Usuario)
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
            ModelState.AddModelError(string.Empty, "Error al registrarse");
            return View(Usuario);
        }

        public ActionResult RecuperarClave(string destinatario)
        {
            try
            {
                string tema = "Reserva de tutorías - Recuperación de clave";
                string claveGenerada = _seguridad.GenerarClave();
                string mensaje = "<h3>Recuperación de contraseña</h3>" +
                    "<p>Has solicitado cambiar la clave de tu usuario debido" +
                    "a que olvidaste la anterior. Para poder iniciar sesión nuevamente" +
                    $"ingresa con tu correo y esta clave generada:<b>{claveGenerada}</b>.</p>" +
                    $"<p>Seguidamente podrás seguir utilizando la misma clave, pero te recomendamos" +
                    $"cambiarla en la sección Mi perfil</p>" +
                    $"<br />" +
                    $"Atte. Equipo de soporte ReservaTutorias.com.";

                var envioCorrecto = _enviaCorreo.EnviarCorreo(destinatario, tema, mensaje);

                if (envioCorrecto)
                {
                    
                    ModelState.AddModelError(string.Empty, "Revisa tu correo e ingresa con la nueva clave");
                    return View("Index");
                }
                ModelState.AddModelError(string.Empty, "No se pudo enviar el correo, intenta nuevamente");
                return View("OlvidaClave");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "No se pudo enviar el correo, intenta nuevamente");
                return View("OlvidaClave");
            }
        }

        private bool ActualizarClave(string destinatario,string claveGenerada)
        {
            Usuario usuario = GetUsers().FirstOrDefault(x => x.Correo == destinatario.Trim());
            usuario.Clave = _seguridad.CreatePwdHash(claveGenerada);
            try
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(usuario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PutAsync("api/Usuario/" + usuario.IdUsuario, byteContent).Result;

                    return postTask.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Usuario> GetUsers()
        {
            List<Usuario> aux = new List<Usuario>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Usuario").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Usuario>>(auxres);
                }
            }
            return aux;
        }

        [AuthorizeView("Administrador,Tutor,Estudiante")]
        public ActionResult MiPerfil()
        {
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            ViewBag.Perfil = sesion.TipoUsuario;
            return View(sesion);
        }

        [AuthorizeView("Administrador,Tutor,Estudiante")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                if(Usuario.Clave == null || Usuario.Clave ==string.Empty)
                {
                    Usuario.Clave = GetUsuarioById(Usuario.IdUsuario).Result.Clave;
                }
                else
                {
                    Usuario.Clave = _seguridad.CreatePwdHash(Usuario.Clave);
                }
                
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
                            HttpContext.Session.SetObject("session", Usuario);
                            return RedirectToAction("Index","Home");
                        }
                    }
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
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
