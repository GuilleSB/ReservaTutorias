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
    public class UsuarioController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public UsuarioController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Usuario>>> GetUsuario()
        {
            return new BS.Usuario(_context).GetAll().ToList();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Usuario>> GetUsuario(int id)
        {
            var Usuario = new BS.Usuario(_context).GetOneById(id);

            if (Usuario == null)
            {
                return NotFound();
            }

            return Usuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Data.Usuario Usuario)
        {
            if (id != Usuario.IdUsuario)
            {
                return BadRequest();
            }

            try
            {
                new BS.Usuario(_context).Update(Usuario);
            }
            catch (Exception)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Usuario>> PostUsuario(Data.Usuario Usuario)
        {
            new BS.Usuario(_context).Insert(Usuario);

            return CreatedAtAction("GetUsuario", new { id = Usuario.IdUsuario }, Usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Usuario>> DeleteUsuario(int id)
        {
            var Usuario = new BS.Usuario(_context).GetOneById(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            new BS.Usuario(_context).Delete(Usuario);

            return Usuario;
        }

        private bool UsuarioExists(int id)
        {
            return (new BS.Usuario(_context).GetOneById(id) != null);
        }
    }

}
