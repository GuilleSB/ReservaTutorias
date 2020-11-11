using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.DAL.EF;
using Data = Backend.DO.Objects;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public TemaController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Tema
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Tema>>> GetTema()
        {
            return new BS.Tema(_context).GetAll().ToList();
        }

        // GET: api/Tema/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Tema>> GetTema(int id)
        {
            var Tema = new BS.Tema(_context).GetOneById(id);

            if (Tema == null)
            {
                return NotFound();
            }

            return Tema;
        }

        // PUT: api/Tema/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTema(int id, Data.Tema Tema)
        {
            if (id != Tema.IdTema)
            {
                return BadRequest();
            }

            try
            {
                new BS.Tema(_context).Update(Tema);
            }
            catch (Exception)
            {
                if (!TemaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tema
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Tema>> PostTema(Data.Tema Tema)
        {
            new BS.Tema(_context).Insert(Tema);

            return CreatedAtAction("GetTema", new { id = Tema.IdTema }, Tema);
        }

        // DELETE: api/Tema/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Tema>> DeleteTema(int id)
        {
            var Tema = new BS.Tema(_context).GetOneById(id);
            if (Tema == null)
            {
                return NotFound();
            }

            new BS.Tema(_context).Delete(Tema);

            return Tema;
        }

        private bool TemaExists(int id)
        {
            return (new BS.Tema(_context).GetOneById(id) != null);
        }
    }

}
