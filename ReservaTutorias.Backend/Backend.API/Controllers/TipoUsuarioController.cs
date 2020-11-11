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
    public class TipoUsuarioController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public TipoUsuarioController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.TipoUsuario>>> GetTipoUsuario()
        {
            return new BS.TipoUsuario(_context).GetAll().ToList();
        }

        // GET: api/TipoUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.TipoUsuario>> GetTipoUsuario(int id)
        {
            var TipoUsuario = new BS.TipoUsuario(_context).GetOneById(id);

            if (TipoUsuario == null)
            {
                return NotFound();
            }

            return TipoUsuario;
        }

        // PUT: api/TipoUsuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsuario(int id, Data.TipoUsuario TipoUsuario)
        {
            if (id != TipoUsuario.IdTipoUsuario)
            {
                return BadRequest();
            }

            try
            {
                new BS.TipoUsuario(_context).Update(TipoUsuario);
            }
            catch (Exception)
            {
                if (!TipoUsuarioExists(id))
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

        // POST: api/TipoUsuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.TipoUsuario>> PostTipoUsuario(Data.TipoUsuario TipoUsuario)
        {
            new BS.TipoUsuario(_context).Insert(TipoUsuario);

            return CreatedAtAction("GetTipoUsuario", new { id = TipoUsuario.IdTipoUsuario }, TipoUsuario);
        }

        // DELETE: api/TipoUsuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.TipoUsuario>> DeleteTipoUsuario(int id)
        {
            var TipoUsuario = new BS.TipoUsuario(_context).GetOneById(id);
            if (TipoUsuario == null)
            {
                return NotFound();
            }

            new BS.TipoUsuario(_context).Delete(TipoUsuario);

            return TipoUsuario;
        }

        private bool TipoUsuarioExists(int id)
        {
            return (new BS.TipoUsuario(_context).GetOneById(id) != null);
        }
    }

}
