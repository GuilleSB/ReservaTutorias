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
    public class HorarioController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public HorarioController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Horario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Horario>>> GetHorario()
        {
            return new BS.Horario(_context).GetAll().ToList();
        }

        // GET: api/Horario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Horario>> GetHorario(int id)
        {
            var Horario = new BS.Horario(_context).GetOneById(id);

            if (Horario == null)
            {
                return NotFound();
            }

            return Horario;
        }

        // PUT: api/Horario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Data.Horario Horario)
        {
            if (id != Horario.IdHorario)
            {
                return BadRequest();
            }

            try
            {
                new BS.Horario(_context).Update(Horario);
            }
            catch (Exception)
            {
                if (!HorarioExists(id))
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

        // POST: api/Horario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Horario>> PostHorario(Data.Horario Horario)
        {
            new BS.Horario(_context).Insert(Horario);

            return CreatedAtAction("GetHorario", new { id = Horario.IdHorario }, Horario);
        }

        // DELETE: api/Horario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Horario>> DeleteHorario(int id)
        {
            var Horario = new BS.Horario(_context).GetOneById(id);
            if (Horario == null)
            {
                return NotFound();
            }

            new BS.Horario(_context).Delete(Horario);

            return Horario;
        }

        private bool HorarioExists(int id)
        {
            return (new BS.Horario(_context).GetOneById(id) != null);
        }
    }

}
