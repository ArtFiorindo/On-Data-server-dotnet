using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnData.Models;
using OnData.Data;
using System.Threading.Tasks;

namespace OnData.Controllers
{
    [Route("Cadastro")]
    public class CadastroController : Controller
    {
        private readonly OnDataDbContext _context;

        public CadastroController(OnDataDbContext context)
        {
            _context = context;
        }

        // Exibe a lista de pacientes e ocorrências
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var pacientes = await _context.Pacientes.Include(p => p.Ocorrencias).ToListAsync();
            return View(pacientes);
        }

        // Exibe o formulário de criação
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new PacienteOcorrenciaViewModel());
        }

        // Processa o formulário de criação (POST)
        [HttpPost("Create")]
        public async Task<IActionResult> Create(PacienteOcorrenciaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Adiciona o paciente ao contexto e salva
                _context.Pacientes.Add(model.Paciente);
                await _context.SaveChangesAsync();

                // Se existir uma ocorrência, associa o ID do paciente e salva
                if (model.Ocorrencia != null)
                {
                    model.Ocorrencia.PacienteId = model.Paciente.Id;
                    _context.Ocorrencias.Add(model.Ocorrencia);
                    await _context.SaveChangesAsync();
                }

                // Redireciona para a lista após salvar
                return RedirectToAction(nameof(Index));
            }

            // Retorna a view com o modelo em caso de erro de validação
            return View(model);
        }

        // Exibe os detalhes de um paciente e sua ocorrência
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            var model = new PacienteOcorrenciaViewModel
            {
                Paciente = paciente,
                Ocorrencia = paciente.Ocorrencias.FirstOrDefault()
            };

            return View(model);
        }

        // Exibe o formulário de edição preenchido com os dados existentes
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            var model = new PacienteOcorrenciaViewModel
            {
                Paciente = paciente,
                Ocorrencia = paciente.Ocorrencias.FirstOrDefault()
            };

            return View(model);
        }

        // Processa o formulário de edição (POST)
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, PacienteOcorrenciaViewModel model)
        {
            if (id != model.Paciente.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.Paciente);

                    if (model.Ocorrencia != null)
                    {
                        model.Ocorrencia.PacienteId = model.Paciente.Id;
                        _context.Update(model.Ocorrencia);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Pacientes.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Exibe a confirmação de exclusão
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            var model = new PacienteOcorrenciaViewModel
            {
                Paciente = paciente,
                Ocorrencia = paciente.Ocorrencias.FirstOrDefault()
            };

            return View(model);
        }

        // Processa a exclusão (POST)
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente != null)
            {
                _context.Ocorrencias.RemoveRange(paciente.Ocorrencias);
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
