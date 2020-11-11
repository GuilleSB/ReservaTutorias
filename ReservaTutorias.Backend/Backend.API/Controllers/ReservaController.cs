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
    public class ReservaController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public ReservaController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Reserva
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Reserva>>> GetReserva()
        {
            return new BS.Reserva(_context).GetAll().ToList();
        }

        // GET: api/Reserva/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Reserva>> GetReserva(int id)
        {
            var Reserva = new BS.Reserva(_context).GetOneById(id);

            if (Reserva == null)
            {
                return NotFound();
            }

            return Reserva;
        }

        // PUT: api/Reserva/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Data.Reserva Reserva)
        {
            if (id != Reserva.IdReserva)
            {
                return BadRequest();
            }

            try
            {
                new BS.Reserva(_context).Update(Reserva);
            }
            catch (Exception)
            {
                if (!ReservaExists(id))
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

        // POST: api/Reserva
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Reserva>> PostReserva(Data.Reserva Reserva)
        {
            new BS.Reserva(_context).Insert(Reserva);

            return CreatedAtAction("GetReserva", new { id = Reserva.IdReserva }, Reserva);
        }

        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Reserva>> DeleteReserva(int id)
        {
            var Reserva = new BS.Reserva(_context).GetOneById(id);
            if (Reserva == null)
            {
                return NotFound();
            }

            new BS.Reserva(_context).Delete(Reserva);

            return Reserva;
        }

        private bool ReservaExists(int id)
        {
            return (new BS.Reserva(_context).GetOneById(id) != null);
        }
    }

}
