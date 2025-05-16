using Microsoft.AspNetCore.Mvc;
using OnData.Services;
using OnData.Models;

namespace OnData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return Ok(pacientes);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return BadRequest("O ID do paciente não coincide.");
            }

            var result = await _pacienteService.UpdatePacienteAsync(id, paciente);

            if (!result)
            {
                return NotFound("Paciente não encontrado.");
            }

            return NoContent();
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            var createdPaciente = await _pacienteService.AddPacienteAsync(paciente);
            return CreatedAtAction(nameof(GetPaciente), new { id = createdPaciente.Id }, createdPaciente);
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var result = await _pacienteService.DeletePacienteAsync(id);

            if (!result)
            {
                return NotFound("Paciente não encontrado.");
            }

            return NoContent();
        }
    }
}