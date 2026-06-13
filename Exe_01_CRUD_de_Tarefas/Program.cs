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

// ================== CONSTRUÇÃO DA APLICAÇÃO ==================

var app = builder.Build();

// Habilita a documentação Swagger e a interface de teste
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Tarefas v1"));

// Redireciona requisições HTTP para HTTPS (se configurado)
app.UseHttpsRedirection();

// ================== DEFINIÇÃO DOS ENDPOINTS ==================

/// <summary>
/// GET /api/tarefas - Retorna todas as tarefas cadastradas
/// </summary>
app.MapGet("/api/tarefas", (ITarefaService tarefaService) =>
{
    var tarefas = tarefaService.ObterTodas();
    return Results.Ok(tarefas);
})
   .WithName("ListarTarefas")
   .WithDescription("Obtém a lista completa de todas as tarefas cadastradas");

/// <summary>
/// GET /api/tarefas/{id} - Obtém uma tarefa específica pelo ID
/// </summary>
app.MapGet("/api/tarefas/{id:int}", (int id, ITarefaService tarefaService) =>
{
    var tarefa = tarefaService.ObterPorId(id);
    if (tarefa is null)
    {
        return Results.NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
    }

    return Results.Ok(tarefa);
})
   .WithName("ObterTarefaPorId")
   .WithDescription("Obtém uma tarefa específica pelo seu identificador");

/// <summary>
/// POST /api/tarefas - Cria uma nova tarefa
/// </summary>
app.MapPost("/api/tarefas", (CriarTarefaRequest request, ITarefaService tarefaService) =>
{
    try
    {
        var tarefa = tarefaService.Criar(request);
        return Results.Created($"/api/tarefas/{tarefa.Id}", tarefa);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
})
   .WithName("CriarTarefa")
   .WithDescription("Cria uma nova tarefa com os dados fornecidos");

/// <summary>
/// PUT /api/tarefas/{id} - Atualiza uma tarefa existente
/// </summary>
app.MapPut("/api/tarefas/{id:int}", (int id, AtualizarTarefaRequest request, ITarefaService tarefaService) =>
{
    var tarefa = tarefaService.Atualizar(id, request);
    if (tarefa is null)
    {
        return Results.NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
    }

    return Results.Ok(tarefa);
})
   .WithName("AtualizarTarefa")
   .WithDescription("Atualiza uma tarefa existente com os dados fornecidos");

/// <summary>
/// DELETE /api/tarefas/{id} - Deleta uma tarefa
/// </summary>
app.MapDelete("/api/tarefas/{id:int}", (int id, ITarefaService tarefaService) =>
{
    var tarefa = tarefaService.Deletar(id);
    if (tarefa is null)
    {
        return Results.NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
    }

    return Results.Ok(new { mensagem = "Tarefa deletada com sucesso", tarefa });
})
   .WithName("DeletarTarefa")
   .WithDescription("Deleta uma tarefa existente");

// ================== EXECUÇÃO ==================

app.Run();
