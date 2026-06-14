using Microsoft.AspNetCore.Mvc;
using Exe_01_CRUD_de_Tarefas.Models;
using Exe_01_CRUD_de_Tarefas.Services;

namespace Exe_01_CRUD_de_Tarefas.Controllers;

/// <summary>
/// Controller responsável pelos endpoints da API de tarefas.
/// Fornece operações CRUD (Create, Read, Update, Delete) para gerenciar tarefas.
/// </summary>
[ApiController]
[Route("api/tarefas")]
[Produces("application/json")]
public class TarefasController : ControllerBase
{
    /// <summary>
    /// Serviço injetado para operações de tarefas.
    /// </summary>
    private readonly ITarefaService _tarefaService;

    /// <summary>
    /// Logger para rastreamento de operações.
    /// </summary>
    private readonly ILogger<TarefasController> _logger;

    public TarefasController(ITarefaService tarefaService, ILogger<TarefasController> logger)
    {
        _tarefaService = tarefaService;
        _logger = logger;
    }

    /// <summary>
    /// Obtém todas as tarefas cadastradas.
    /// </summary>
    /// <returns>Lista de tarefas.</returns>
    /// <response code="200">Retorna a lista de tarefas.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Tarefa>> ObterTodas()
    {
        _logger.LogInformation("Solicitação para listar todas as tarefas");
        var tarefas = _tarefaService.ObterTodas();
        return Ok(tarefas);
    }

    /// <summary>
    /// Obtém uma tarefa específica pelo seu identificador.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser obtida.</param>
    /// <returns>Os dados da tarefa encontrada.</returns>
    /// <response code="200">Tarefa encontrada e retornada.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public ActionResult<Tarefa> ObterPorId(int id)
    {
        _logger.LogInformation($"Solicitação para obter tarefa com ID: {id}");
        var tarefa = _tarefaService.ObterPorId(id);
        
        if (tarefa is null)
        {
            _logger.LogWarning($"Tarefa com ID {id} não encontrada");
            return NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
        }

        return Ok(tarefa);
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="request">Dados para criar a tarefa.</param>
    /// <returns>A tarefa criada com seu ID.</returns>
    /// <response code="201">Tarefa criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou título vazio.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public ActionResult<Tarefa> Criar([FromBody] CriarTarefaRequest request)
    {
        _logger.LogInformation($"Solicitação para criar tarefa: {request.Titulo}");

        try
        {
            var tarefa = _tarefaService.Criar(request);
            _logger.LogInformation($"Tarefa criada com sucesso. ID: {tarefa.Id}");
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning($"Erro ao criar tarefa: {ex.Message}");
            return BadRequest(new { erro = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser atualizada.</param>
    /// <param name="request">Dados para atualizar a tarefa.</param>
    /// <returns>A tarefa atualizada.</returns>
    /// <response code="200">Tarefa atualizada com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public ActionResult<Tarefa> Atualizar(int id, [FromBody] AtualizarTarefaRequest request)
    {
        _logger.LogInformation($"Solicitação para atualizar tarefa com ID: {id}");
        
        var tarefa = _tarefaService.Atualizar(id, request);
        if (tarefa is null)
        {
            _logger.LogWarning($"Tarefa com ID {id} não encontrada para atualização");
            return NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
        }

        _logger.LogInformation($"Tarefa com ID {id} atualizada com sucesso");
        return Ok(tarefa);
    }

    /// <summary>
    /// Deleta uma tarefa existente.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser deletada.</param>
    /// <returns>Mensagem de sucesso e a tarefa deletada.</returns>
    /// <response code="200">Tarefa deletada com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public ActionResult<object> Deletar(int id)
    {
        _logger.LogInformation($"Solicitação para deletar tarefa com ID: {id}");
        
        var tarefa = _tarefaService.Deletar(id);
        if (tarefa is null)
        {
            _logger.LogWarning($"Tarefa com ID {id} não encontrada para exclusão");
            return NotFound(new { mensagem = $"Tarefa com ID {id} não encontrada" });
        }

        _logger.LogInformation($"Tarefa com ID {id} deletada com sucesso");
        return Ok(new { mensagem = "Tarefa deletada com sucesso", tarefa });
    }
}
