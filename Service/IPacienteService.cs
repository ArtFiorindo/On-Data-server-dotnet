using OnData.Data;
using OnData.Models;
using Microsoft.EntityFrameworkCore;

namespace OnData.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> GetAllPacientesAsync();
        Task<Paciente?> GetPacienteByIdAsync(int id);
        Task<Paciente> AddPacienteAsync(Paciente paciente);
        Task<bool> UpdatePacienteAsync(int id, Paciente paciente);
        Task<bool> DeletePacienteAsync(int id);
    }

    public class PacienteService : IPacienteService
    {
        private readonly OnDataDbContext _context;

        public PacienteService(OnDataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetAllPacientesAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente?> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<Paciente> AddPacienteAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<bool> UpdatePacienteAsync(int id, Paciente paciente)
        {
            if (id != paciente.Id) return false;

            _context.Entry(paciente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeletePacienteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return false;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}