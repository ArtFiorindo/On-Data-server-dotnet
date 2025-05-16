using Microsoft.EntityFrameworkCore;
using OnData.Data;
using OnData.Services;
using ConfigurationManager = OnData.Services.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

// Inicializar o ConfigurationManager como Singleton
var configManager = ConfigurationManager.Instance(builder.Configuration);

// Configuração das URLs, incluindo as portas HTTP e HTTPS
builder.WebHost.UseUrls("http://localhost:5146", "https://localhost:7114");

// Configuração dos serviços, agora com suporte a Views
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnDataDbContext>(options =>
    options.UseOracle(configManager.GetConnectionString("OracleDbConnection")));

// Registro do serviço de Paciente para injeção de dependência
builder.Services.AddScoped<IPacienteService, PacienteService>();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita o uso de arquivos estáticos na pasta wwwroot
app.UseStaticFiles();

// Habilitar o redirecionamento para HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

// Configuração das rotas padrão para controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();