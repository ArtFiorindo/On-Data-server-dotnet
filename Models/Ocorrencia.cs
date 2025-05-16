using System;

namespace OnData.Models
{
    public class Ocorrencia
    {
        public int IdOcorrencia { get; set; }  // Propriedade da chave primária

        public DateTime? Data { get; set; }
        public decimal? Valor { get; set; }
        public bool? Aprovado { get; set; }

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }
    }
}