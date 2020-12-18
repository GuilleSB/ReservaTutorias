using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ReservaTutorias.Frontend.Models;
using ReservaTutorias.Frontend.Utils.Filters;

namespace ReservaTutorias.Frontend.Controllers
{
    [AuthorizeView("Tutor,Administrador")]
    public class TemaController : Controller
    {
        string baseurl = "http://panchoalambra-001-site1.ftempurl.com/";
        // GET: Tema
        public async Task<IActionResult> Index()
        {
            List<Tema> aux = new List<Tema>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Tema");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Tema>>(auxres);
                }
            }
            return View(aux);
        }

        // GET: Tema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tema = await GetTemaById(id);
            Tema.Materia = GetAllMateria().SingleOrDefault(x=> x.IdMateria == Tema.IdMateria);
            if (Tema == null)
            {
                return NotFound();
            }

            return View(Tema);
        }

        // GET: Tema/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdMateria"] = new SelectList(GetAllMateria(), "IdMateria", "NombreMateria");
            return View();
        }

        // POST: Tema/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tema Tema)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(Tema);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/Tema", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewData["IdMateria"] = new SelectList(GetAllMateria(), "IdMateria", "NombreMateria");
            ModelState.AddModelError(string.Empty, "Error al crear el tema");
            return View(Tema);
        }

        // GET: Tema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tema = await GetTemaById(id);
            if (Tema == null)
            {
                return NotFound();
            }
            ViewData["IdMateria"] = new SelectList(GetAllMateria(), "IdMateria", "NombreMateria");
            return View(Tema);
        }

        // POST: Tema/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind] Tema Tema)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(Tema);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/Tema/" + Tema.IdTema, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var auxTema = await GetTemaById(id);
                    if (auxTema == null)
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
            ViewData["IdMateria"] = new SelectList(GetAllMateria(), "IdMateria", "NombreMateria");
            return View(Tema);
        }

        // GET: Tema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tema = await GetTemaById(id);
            Tema.Materia = GetAllMateria().SingleOrDefault(x => x.IdMateria == Tema.IdMateria);
            if (Tema == null)
            {
                return NotFound();
            }
            return View(Tema);
        }

        // POST: Tema/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage res = await cl.GetAsync("api/Tema/5?"+id);
                HttpResponseMessage res = await cl.DeleteAsync("api/Tema/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        private async Task<Tema> GetTemaById(int? id)
        {
            Tema aux = new Tema();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/Tema/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<Tema>(auxres);
                }
            }
            return aux;
        }

        private List<Materia> GetAllMateria()
        {
            List<Materia> aux = new List<Materia>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Materia").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<Materia>>(auxres);
                }
            }
            return aux;
        }
    }
}
