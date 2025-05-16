// Paciente.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnData.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

        // Navegação para Ocorrências
        public ICollection<Ocorrencia> Ocorrencias { get; set; } = new List<Ocorrencia>();
    }
}