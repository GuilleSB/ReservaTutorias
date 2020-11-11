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
    public class EntradaController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public EntradaController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Entrada
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Entrada>>> GetEntrada()
        {
            return new BS.Entrada(_context).GetAll().ToList();
        }

        // GET: api/Entrada/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Entrada>> GetEntrada(int id)
        {
            var Entrada = new BS.Entrada(_context).GetOneById(id);

            if (Entrada == null)
            {
                return NotFound();
            }

            return Entrada;
        }

        // PUT: api/Entrada/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrada(int id, Data.Entrada Entrada)
        {
            if (id != Entrada.IdEntrada)
            {
                return BadRequest();
            }

            try
            {
                new BS.Entrada(_context).Update(Entrada);
            }
            catch (Exception)
            {
                if (!EntradaExists(id))
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

        // POST: api/Entrada
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Entrada>> PostEntrada(Data.Entrada Entrada)
        {
            new BS.Entrada(_context).Insert(Entrada);

            return CreatedAtAction("GetEntrada", new { id = Entrada.IdEntrada }, Entrada);
        }

        // DELETE: api/Entrada/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Entrada>> DeleteEntrada(int id)
        {
            var Entrada = new BS.Entrada(_context).GetOneById(id);
            if (Entrada == null)
            {
                return NotFound();
            }

            new BS.Entrada(_context).Delete(Entrada);

            return Entrada;
        }

        private bool EntradaExists(int id)
        {
            return (new BS.Entrada(_context).GetOneById(id) != null);
        }
    }
    
}
