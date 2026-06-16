using Exe_01_CRUD_de_Tarefas.Models;

/// <summary>
/// Serviço responsável pela lógica de negócio das operações de tarefas.
/// Implementa operações CRUD (Create, Read, Update, Delete) para gerenciar tarefas.
/// </summary>
namespace Exe_01_CRUD_de_Tarefas.Services;

public interface ITarefaService
{
    IEnumerable<Tarefa> ObterTodas();

    Tarefa? ObterPorId(int id);

    Tarefa Criar(CriarTarefaRequest request);

    Tarefa? Atualizar(int id, AtualizarTarefaRequest request);

    Tarefa? Deletar(int id);
}

/// <summary>
/// Implementação do serviço de tarefas com armazenamento em memória.
/// </summary>
public class TarefaService : ITarefaService
{
    /// <summary>
    /// Repositório em memória das tarefas (em produção, seria um banco de dados).
    /// </summary>
    private readonly List<Tarefa> _tarefas = new();

    private int _proximoId = 1;

    public IEnumerable<Tarefa> ObterTodas()
    {
        return _tarefas.AsReadOnly();
    }

    public Tarefa? ObterPorId(int id)
    {
        return _tarefas.FirstOrDefault(t => t.Id == id);
    }

    public Tarefa Criar(CriarTarefaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            throw new ArgumentException("O título da tarefa é obrigatório.");
        }

        var tarefa = new Tarefa
        {
            Id = _proximoId++,
            Titulo = request.Titulo.Trim(),
            Descricao = request.Descricao,
            Concluida = false,
            CriadaEm = DateTime.UtcNow
        };

        _tarefas.Add(tarefa);
        return tarefa;
    }

    public Tarefa? Atualizar(int id, AtualizarTarefaRequest request)
    {
        var tarefa = ObterPorId(id);
        if (tarefa is null)
        {
            return null;
        }

        if (!string.IsNullOrWhiteSpace(request.Titulo))
        {
            tarefa.Titulo = request.Titulo.Trim();
        }

        if (request.Descricao is not null)
        {
            tarefa.Descricao = request.Descricao;
        }

        if (request.Concluida.HasValue)
        {
            tarefa.Concluida = request.Concluida.Value;
        }

        return tarefa;
    }

    public Tarefa? Deletar(int id)
    {
        var tarefa = ObterPorId(id);
        if (tarefa is null)
        {
            return null;
        }

        _tarefas.Remove(tarefa);
        return tarefa;
    }
}
