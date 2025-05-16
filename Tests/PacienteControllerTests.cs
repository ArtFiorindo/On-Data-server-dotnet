using Microsoft.AspNetCore.Mvc;
using Moq;
using OnData.Controllers;
using OnData.Models;
using OnData.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnData.Tests
{
    public class PacienteControllerTests
    {
        private readonly Mock<IPacienteService> _mockService;
        private readonly PacienteController _controller;

        public PacienteControllerTests()
        {
            _mockService = new Mock<IPacienteService>();
            _controller = new PacienteController(_mockService.Object);
        }

        [Fact]
        public async Task GetPacientes_ReturnsListOfPacientes()
        {
            // Configuração de pacientes simulados
            var pacientes = new List<Paciente>
            {
                new Paciente { Id = 1, Nome = "Paciente 1" },
                new Paciente { Id = 2, Nome = "Paciente 2" }
            };
            _mockService.Setup(s => s.GetAllPacientesAsync()).ReturnsAsync(pacientes);

            // Executa o método do controlador
            var result = await _controller.GetPacientes();

            // Verifica se o resultado é do tipo OkObjectResult
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Extrai o valor e verifica o conteúdo
            var returnedPacientes = Assert.IsAssignableFrom<IEnumerable<Paciente>>(okResult.Value);
            Assert.Equal(2, returnedPacientes.Count());
        }

        [Fact]
        public async Task GetPaciente_ReturnsNotFound_WhenPacienteDoesNotExist()
        {
            // Configuração de retorno nulo
            _mockService.Setup(s => s.GetPacienteByIdAsync(1)).ReturnsAsync((Paciente?)null);

            // Executa o método do controlador
            var result = await _controller.GetPaciente(1);

            // Valida que o resultado é do tipo NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}