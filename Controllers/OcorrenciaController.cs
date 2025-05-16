using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnData.Data;
using OnData.Models;

namespace OnData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcorrenciaController : ControllerBase
    {
        private readonly OnDataDbContext _context;

        public OcorrenciaController(OnDataDbContext context)
        {
            _context = context;
        }

        // GET: api/Ocorrencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ocorrencia>>> GetOcorrencias()
        {
            return await _context.Ocorrencias.ToListAsync();
        }

        // GET: api/Ocorrencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ocorrencia>> GetOcorrencia(int id)
        {
            var Ocorrencia = await _context.Ocorrencias.FindAsync(id);

            if (Ocorrencia == null)
            {
                return NotFound();
            }

            return Ocorrencia;
        }

        // PUT: api/Ocorrencias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOcorrencia(int id, Ocorrencia Ocorrencia)
        {
            if (id != Ocorrencia.IdOcorrencia)
            {
                return BadRequest();
            }

            _context.Entry(Ocorrencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcorrenciaExists(id))
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

        // POST: api/Ocorrencias
        [HttpPost]
        public async Task<ActionResult<Ocorrencia>> PostOcorrencia(Ocorrencia Ocorrencia)
        {
            _context.Ocorrencias.Add(Ocorrencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOcorrencia), new { id = Ocorrencia.IdOcorrencia }, Ocorrencia);
        }

        // DELETE: api/Ocorrencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOcorrencia(int id)
        {
            var Ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (Ocorrencia == null)
            {
                return NotFound();
            }

            _context.Ocorrencias.Remove(Ocorrencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OcorrenciaExists(int id)
        {
            return _context.Ocorrencias.Any(e => e.IdOcorrencia == id);
        }
    }
}
