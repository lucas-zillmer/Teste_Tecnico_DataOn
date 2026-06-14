using Exe_01_CRUD_de_Tarefas.Models;
using Exe_01_CRUD_de_Tarefas.Services;
using Microsoft.OpenApi.Models;

/// <summary>
/// Configuração e execução da API de gerenciamento de tarefas.
/// A aplicação fornece endpoints RESTful para operações CRUD de tarefas.
/// </summary>

// ================== CONFIGURAÇÃO DA APLICAÇÃO ==================

var builder = WebApplication.CreateBuilder(args);

// Define a URL e porta em que a aplicação escuta
builder.WebHost.UseUrls("http://127.0.0.1:5001");

// ================== REGISTRO DE SERVIÇOS ==================

// Registra o serviço de tarefas como singleton (instância única por ciclo de vida da aplicação)
builder.Services.AddSingleton<ITarefaService, TarefaService>();

// Adiciona suporte a controllers
builder.Services.AddControllers();

// Adiciona suporte a descoberta de endpoints e geração de documentação Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Tarefas",
        Version = "v1",
        Description = "API RESTful para gerenciar tarefas com operações CRUD completas"
    });
});

// Adiciona logging
builder.Services.AddLogging();

// ================== CONSTRUÇÃO DA APLICAÇÃO ==================

var app = builder.Build();

// Habilita a documentação Swagger e a interface de teste
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Tarefas v1"));

// Redireciona requisições HTTP para HTTPS (se configurado)
app.UseHttpsRedirection();

// Habilita a autorização
app.UseAuthorization();

// Mapeia os controllers
app.MapControllers();

// ================== EXECUÇÃO ==================

app.Run();
