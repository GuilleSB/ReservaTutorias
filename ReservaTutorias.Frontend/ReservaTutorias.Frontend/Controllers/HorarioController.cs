using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Models.ModelViews;
using ReservaTutorias.Frontend.Utils.Extensions;
using ReservaTutorias.Frontend.Utils.Filters;

namespace ReservaTutorias.Frontend.Controllers
{
    [AuthorizeView("Tutor,Administrador")]
    public class HorarioController : Controller
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        // GET: Horario
        public async Task<IActionResult> Index()
        {
            List<Horario> aux = new List<Horario>();
            List<ViewHorario> mapaux = new List<ViewHorario>();
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Horario");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Horario>>(auxres);
                    aux = aux.FindAll(x => x.IdTutor == sesion.IdUsuario); // Horarios usuario actual
                    mapaux = MapViewHorarioList(aux);
                }
            }
            return View(mapaux);
        }

        // GET: Horario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Horario = await GetHorarioById(id);
            ViewBag.Matriculados = GetEstudiantesConReserva(id.Value);
            if (Horario == null)
            {
                return NotFound();
            }

            return View(Horario);
        }

        // GET: Horario/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdTema"] = new SelectList(
                (from s in GetAllTema()
                 select new
                 {
                     s.IdTema,
                     MateriaTema = s.Materia.NombreMateria + " - " + s.NombreTema
                 })
                , "IdTema"
                , "MateriaTema");
            return View();
        }

        // POST: Horario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Horario Horario)
        {
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            Horario.IdTutor = sesion.IdUsuario;
            var horaValida = Horario.FechaHora > DateTime.Now;
            
            //Carga combo en caso de error
            ViewData["IdTema"] = new SelectList(
                (from s in GetAllTema()
                 select new
                 {
                     s.IdTema,
                     MateriaTema = s.Materia.NombreMateria + " - " + s.NombreTema
                 })
                , "IdTema"
                , "MateriaTema");


            if (!horaValida)
            {
                ModelState.AddModelError(string.Empty, "La hora debe ser mayor a la actual");
                return View(Horario);
            }
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(Horario);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Horario", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Error al crear el horario");
            return View(Horario);
        }

        // GET: Horario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ViewHorario = await GetHorarioById(id);
            var Horario = ViewHorario.Horario;
            if (Horario == null)
            {
                return NotFound();
            }
            ViewData["IdTema"] = new SelectList(
                (from s in GetAllTema()
                 select new
                 {
                     s.IdTema,
                     MateriaTema = s.Materia.NombreMateria + " - " + s.NombreTema
                 })
                , "IdTema"
                , "MateriaTema");
            return View(Horario);
        }

        // POST: Horario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Horario Horario)
        {
            var sesion = HttpContext.Session.GetObject<Usuario>("session");
            Horario.IdTutor = sesion.IdUsuario;

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(Horario);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Horario/" + Horario.IdHorario, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var auxHorario = await GetHorarioById(id);
                    if (auxHorario == null)
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
            ViewData["IdTema"] = new SelectList(
                (from s in GetAllTema()
                 select new
                 {
                     s.IdTema,
                     MateriaTema = s.Materia.NombreMateria + " - " + s.NombreTema
                 })
                , "IdTema"
                , "MateriaTema");
            ModelState.AddModelError(string.Empty, "Error al editar el horario");
            return View(Horario);
        }

        // GET: Horario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Horario = await GetHorarioById(id);
            if (Horario == null)
            {
                return NotFound();
            }
            return View(Horario);
        }

        // POST: Horario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage res = await cl.GetAsync("api/Horario/5?"+id);
                HttpResponseMessage res = await cl.DeleteAsync("api/Horario/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }



        private async Task<ViewHorario> GetHorarioById(int? id)
        {
            Horario aux = new Horario();
            ViewHorario mapaux = new ViewHorario();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Horario/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<Horario>(auxres);
                    mapaux = MapViewHorarioSingle(aux);
                }
            }
            return mapaux;
        }


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
        
        private List<Reserva> GetAllReservas()
        {
            List<Reserva> aux = new List<Reserva>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Reserva").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Reserva>>(auxres);
                }
            }
            return aux;
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

        private List<string> GetEstudiantesConReserva(int idHorario)
        {
            try
            {
                var reservasMiCurso = GetAllReservas().FindAll(x => x.IdHorario == idHorario);
                var listEstudiantes = GetAllTutor()
                    .FindAll(y =>
                    reservasMiCurso.Select(x => x.IdEstudiante).ToList().Contains(y.IdUsuario))
                    .Select(v => v.Cedula + " " + v.Nombre + " " + v.Apellidos).ToList();

                return listEstudiantes;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
