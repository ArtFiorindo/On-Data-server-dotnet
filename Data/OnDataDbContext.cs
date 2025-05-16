using Microsoft.EntityFrameworkCore;
using OnData.Models;

namespace OnData.Data
{
    public class OnDataDbContext : DbContext
    {
        public OnDataDbContext(DbContextOptions<OnDataDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define 'IdOcorrencia' como chave primária para a entidade 'Ocorrencia'
            modelBuilder.Entity<Ocorrencia>()
                .HasKey(o => o.IdOcorrencia);

            // Mapear booleano 'Aprovado' para NUMBER(1)
            modelBuilder.Entity<Ocorrencia>()
                .Property(b => b.Aprovado)
                .HasConversion(
                    v => v.HasValue && v.Value ? 1 : 0,  // Converte true para 1, false/nulo para 0
                    v => v == 1 ? (bool?)true : (bool?)false
                );

            // Outros mapeamentos, se houver
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
    }
}