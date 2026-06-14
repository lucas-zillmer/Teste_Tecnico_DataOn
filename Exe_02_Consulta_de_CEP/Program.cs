using Exe_02_Consulta_de_CEP.Models;
using Exe_02_Consulta_de_CEP.Services;
using Microsoft.OpenApi.Models;

/// <summary>
/// Configuração e execução da API de consulta de endereços por CEP.
/// A aplicação fornece um endpoint RESTful para consultar endereços através do CEP (Código de Endereçamento Postal).
/// Integra-se com o serviço externo ViaCep para obtenção dos dados.
/// </summary>

// ================== CONFIGURAÇÃO DA APLICAÇÃO ==================

var builder = WebApplication.CreateBuilder(args);

// Define a URL e porta em que a aplicação escuta
builder.WebHost.UseUrls("http://127.0.0.1:5002");

// ================== REGISTRO DE SERVIÇOS ==================

// Registra o HttpClient configurado para comunicação com a API ViaCep
builder.Services.AddHttpClient("ViaCep", client =>
{
    // Define a URL base do serviço ViaCep
    client.BaseAddress = new Uri("https://viacep.com.br/");
    // Define timeout de 10 segundos para as requisições
    client.Timeout = TimeSpan.FromSeconds(10);
});

// Registra o serviço de endereço como singleton
builder.Services.AddSingleton<IServicoEndereco, ServicoEndereco>();

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Adiciona logging para rastreamento de operações
builder.Services.AddLogging();

// Adiciona suporte a descoberta de endpoints e geração de documentação Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Consulta de CEP",
        Version = "v1",
        Description = "API RESTful para consultar endereços brasileiros através do CEP"
    });
});

// ================== CONSTRUÇÃO DA APLICAÇÃO ==================

var app = builder.Build();

// Habilita a documentação Swagger e a interface de teste
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Consulta de CEP v1"));

// Habilita a autorização
app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

// ================== EXECUÇÃO ==================

app.Run();
