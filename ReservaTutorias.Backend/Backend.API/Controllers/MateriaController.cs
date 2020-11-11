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
    public class MateriaController : ControllerBase
    {
        private readonly BackendDbContext _context;

        public MateriaController(BackendDbContext context)
        {
            _context = context;
        }

        // GET: api/Materia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Materia>>> GetMateria()
        {
            return new BS.Materia(_context).GetAll().ToList();
        }

        // GET: api/Materia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Materia>> GetMateria(int id)
        {
            var Materia = new BS.Materia(_context).GetOneById(id);

            if (Materia == null)
            {
                return NotFound();
            }

            return Materia;
        }

        // PUT: api/Materia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, Data.Materia Materia)
        {
            if (id != Materia.IdMateria)
            {
                return BadRequest();
            }

            try
            {
                new BS.Materia(_context).Update(Materia);
            }
            catch (Exception)
            {
                if (!MateriaExists(id))
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

        // POST: api/Materia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Data.Materia>> PostMateria(Data.Materia Materia)
        {
            new BS.Materia(_context).Insert(Materia);

            return CreatedAtAction("GetMateria", new { id = Materia.IdMateria }, Materia);
        }

        // DELETE: api/Materia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Data.Materia>> DeleteMateria(int id)
        {
            var Materia = new BS.Materia(_context).GetOneById(id);
            if (Materia == null)
            {
                return NotFound();
            }

            new BS.Materia(_context).Delete(Materia);

            return Materia;
        }

        private bool MateriaExists(int id)
        {
            return (new BS.Materia(_context).GetOneById(id) != null);
        }
    }

}
