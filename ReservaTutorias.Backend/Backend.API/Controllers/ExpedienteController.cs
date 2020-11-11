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
    public class ExpedienteController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public ExpedienteController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Expediente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Expediente>>> GetExpediente()
        {
            return new BS.Expediente(_context).GetAll().ToList();
        }

        // GET: api/Expediente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Expediente>> GetExpediente(int id)
        {
            var Expediente = new BS.Expediente(_context).GetOneById(id);

            if (Expediente == null)
            {
                return NotFound();
            }

            return Expediente;
        }

        // PUT: api/Expediente/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpediente(int id, Data.Expediente Expediente)
        {
            if (id != Expediente.IdExpediente)
            {
                return BadRequest();
            }

            try
            {
                new BS.Expediente(_context).Update(Expediente);
            }
            catch (Exception)
            {
                if (!ExpedienteExists(id))
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

        // POST: api/Expediente
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Expediente>> PostExpediente(Data.Expediente Expediente)
        {
            new BS.Expediente(_context).Insert(Expediente);

            return CreatedAtAction("GetExpediente", new { id = Expediente.IdExpediente }, Expediente);
        }

        // DELETE: api/Expediente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Expediente>> DeleteExpediente(int id)
        {
            var Expediente = new BS.Expediente(_context).GetOneById(id);
            if (Expediente == null)
            {
                return NotFound();
            }

            new BS.Expediente(_context).Delete(Expediente);

            return Expediente;
        }

        private bool ExpedienteExists(int id)
        {
            return (new BS.Expediente(_context).GetOneById(id) != null);
        }
    }

}
