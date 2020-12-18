using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.DAL.EF;
using Data = Backend.DO.Objects;
using AutoMapper;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemaController : ControllerBase
    {
        private readonly BackendDbContext _context;
        private readonly IMapper _mapper;

        public TemaController(BackendDbContext context, IMapper mapper)
        {
            this._mapper = mapper;
            _context = context;
        }

        // GET: api/Tema
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModels.Tema>>> GetTema()
        {
            List<DataModels.Tema> mapaux;
            try
            {
                var aux = await new BS.Tema(_context).GetAllInclude();
                mapaux = _mapper.Map<IEnumerable<Data.Tema>, IEnumerable<DataModels.Tema>>(aux).ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
            return mapaux;
        }

        // GET: api/Tema/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModels.Tema>> GetTema(int id)
        {
            var Tema = new BS.Tema(_context).GetOneById(id);
            var mapaux = _mapper.Map<Data.Tema,DataModels.Tema>(Tema);
            if (mapaux == null)
            {
                return NotFound();
            }

            return mapaux;
        }

        // PUT: api/Tema/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTema(int id, DataModels.Tema Tema)
        {
            if (id != Tema.IdTema)
            {
                return BadRequest();
            }
            var mapaux = _mapper.Map<DataModels.Tema, Data.Tema>(Tema);
            try
            {
                new BS.Tema(_context).Update(mapaux);
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
        public async Task<ActionResult<Data.Tema>> PostTema(DataModels.Tema Tema)
        {
            try
            {
                var mapaux = _mapper.Map<DataModels.Tema, Data.Tema>(Tema);
                new BS.Tema(_context).Insert(mapaux);

                return CreatedAtAction("GetTema", new { id = Tema.IdTema }, Tema);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        // DELETE: api/Tema/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModels.Tema>> DeleteTema(int id)
        {
            var Tema = new BS.Tema(_context).GetOneById(id);
            if (Tema == null)
            {
                return NotFound();
            }

            new BS.Tema(_context).Delete(Tema);
            var mapaux = _mapper.Map<Data.Tema, DataModels.Tema>(Tema);
            return mapaux;
        }

        private bool TemaExists(int id)
        {
            return (new BS.Tema(_context).GetOneById(id) != null);
        }
    }

}
