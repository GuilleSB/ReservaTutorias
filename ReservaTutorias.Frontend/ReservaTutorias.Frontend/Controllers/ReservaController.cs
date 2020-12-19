using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Models.ModelViews;
using ReservaTutorias.Frontend.Utils.Extensions;
using ReservaTutorias.Frontend.Utils.Filters;

namespace ReservaTutorias.Frontend.Controllers
{
    [AuthorizeView("Estudiante")]
    public class ReservaController : Controller
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            List<Reserva> aux = new List<Reserva>();
            List<ViewReserva> mapaux = new List<ViewReserva>();
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Reserva");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Reserva>>(auxres);
                    var horarios = GetAllHorario();

                    // Reservas del usuario logueado
                    mapaux = MapViewReservaList(aux.FindAll(x=>x.IdEstudiante == sesion.IdUsuario));

                    ViewBag.AllReservas = aux.Select(x => x.IdHorario).ToList(); ; // Todas los idhorarios existentes en reservas

                    ViewBag.Horarios = MapViewHorarioList(horarios // Solo los mayores a la fecha actual y que no ha reservado
                        .Where(x=>x.FechaHora > DateTime.Now 
                                && !mapaux.Select(y=>y.Horario.Horario.IdHorario).Contains(x.IdHorario))
                        .ToList());
                    
                }
            }
            return View(mapaux);
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var reservaView = new ViewReserva();
            try
            {
                if (id == null)
                {
                    throw new Exception();
                }

                var Reserva = await GetReservaById(id);
                if (Reserva == null)
                {
                    throw new Exception();
                }

                reservaView = MapViewReservaSingle(Reserva);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return PartialView("_VerReserva", reservaView);
        }

        // GET: Reserva/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Reserva/Create
        [HttpPost]
        public ActionResult Create(string reservaData)
        {
            var Reservas = JsonConvert.DeserializeObject<List<Reserva>>(reservaData);
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                HttpResponseMessage postTask = null;
                foreach(var Reserva in Reservas)
                {
                    Reserva.IdEstudiante = sesion.IdUsuario; // Usuario actual
                    var content = JsonConvert.SerializeObject(Reserva);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    postTask = cl.PostAsync("api/Reserva", byteContent).Result;
                }
                return Json(new { ok = postTask == null ? false : postTask.IsSuccessStatusCode });
            }
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Reserva = await GetReservaById(id);
            if (Reserva == null)
            {
                return NotFound();
            }
            return View(Reserva);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([Bind] Reserva Reserva)
        {
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            var auxReserva = await GetReservaById(Reserva.IdReserva);
            auxReserva.IdEstudiante = sesion.IdUsuario;
            auxReserva.Notas = Reserva.Notas;
            try
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(auxReserva);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PutAsync("api/Reserva/" + auxReserva.IdReserva, byteContent).Result;

                    return Json(new { ok = postTask.IsSuccessStatusCode });
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Reserva = MapViewReservaSingle(await GetReservaById(id));
            if (Reserva == null)
            {
                return NotFound();
            }
            return View(Reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage res = await cl.GetAsync("api/Reserva/5?"+id);
                HttpResponseMessage res = await cl.DeleteAsync("api/Reserva/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            var Reserva = MapViewReservaSingle(await GetReservaById(id));
            ModelState.AddModelError(string.Empty, "Error al eliminar la reserva");
            return View(Reserva);
        }



        private async Task<Reserva> GetReservaById(int? id)
        {
            Reserva aux = new Reserva();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Reserva/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<Reserva>(auxres);
                }
            }
            return aux;
        }

        // Obtiene todos los horarios
        private List<Horario> GetAllHorario()
        {
            List<Horario> aux = new List<Horario>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Horario").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Horario>>(auxres);
                }
            }
            return aux;
        }
        #region Mapeo a modelo de vista
        private List<ViewReserva> MapViewReservaList(List<Reserva> reserva)
        {
            List<ViewReserva> viewHorarios = new List<ViewReserva>();

            reserva.ForEach(x =>
            {
                viewHorarios.Add(new ViewReserva
                {
                    Reserva = x,
                    Horario = MapViewHorarioSingle(
                        GetAllHorario()
                        .SingleOrDefault(y=>y.IdHorario == x.IdHorario))
                });
            });

            return viewHorarios;
        }

        private ViewReserva MapViewReservaSingle(Reserva reserva)
        {
            return new ViewReserva
            {
                Reserva = reserva,
                Horario = MapViewHorarioSingle(GetAllHorario().SingleOrDefault(x => x.IdHorario == reserva.IdHorario))
            };
        }
        private List<ViewHorario> MapViewHorarioList(List<Horario> horario)
        {
            List<ViewHorario> viewHorarios = new List<ViewHorario>();

            horario.ForEach(x =>
            {
                viewHorarios.Add(new ViewHorario
                {
                    Horario = x,
                    Tema = GetAllTema().SingleOrDefault(y => y.IdTema == x.IdTema),
                    Tutor = GetAllTutor().SingleOrDefault(y => y.IdUsuario == x.IdTutor)
                });
            });

            return viewHorarios;
        }
        private ViewHorario MapViewHorarioSingle(Horario horario)
        {
            return new ViewHorario
            {
                Horario = horario,
                Tema = GetAllTema().SingleOrDefault(y => y.IdTema == horario.IdTema),
                Tutor = GetAllTutor().SingleOrDefault(y => y.IdUsuario == horario.IdTutor)
            };
        }
        #endregion

        #region Auxiliares para mapeos
        private List<Tema> GetAllTema()
        {
            List<Tema> aux = new List<Tema>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Tema").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Tema>>(auxres);
                }
            }
            return aux.OrderBy(x => x.Materia.NombreMateria).ToList(); ;
        }

        private List<Usuario> GetAllTutor()
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
        #endregion
    }
}
