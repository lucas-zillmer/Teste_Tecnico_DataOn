using Exe_01_CRUD_de_Tarefas.Models;

/// <summary>
/// Serviço responsável pela lógica de negócio das operações de tarefas.
/// Implementa operações CRUD (Create, Read, Update, Delete) para gerenciar tarefas.
/// </summary>
namespace Exe_01_CRUD_de_Tarefas.Services;

public interface ITarefaService
{
    /// <summary>
    /// Retorna todas as tarefas cadastradas.
    /// </summary>
    /// <returns>Coleção de tarefas.</returns>
    IEnumerable<Tarefa> ObterTodas();

    /// <summary>
    /// Obtém uma tarefa específica pelo seu identificador.
    /// </summary>
    /// <param name="id">O identificador da tarefa.</param>
    /// <returns>A tarefa encontrada ou null se não existir.</returns>
    Tarefa? ObterPorId(int id);

    /// <summary>
    /// Cria uma nova tarefa e adiciona ao repositório.
    /// </summary>
    /// <param name="request">Os dados para criar a tarefa.</param>
    /// <returns>A tarefa criada com ID atribuído.</returns>
    Tarefa Criar(CriarTarefaRequest request);

    /// <summary>
    /// Atualiza uma tarefa existente com novos dados.
    /// </summary>
    /// <param name="id">O identificador da tarefa a atualizar.</param>
    /// <param name="request">Os dados para atualizar a tarefa.</param>
    /// <returns>A tarefa atualizada ou null se não existir.</returns>
    Tarefa? Atualizar(int id, AtualizarTarefaRequest request);

    /// <summary>
    /// Deleta uma tarefa do repositório.
    /// </summary>
    /// <param name="id">O identificador da tarefa a deletar.</param>
    /// <returns>A tarefa deletada ou null se não existir.</returns>
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

    /// <summary>
    /// Próximo ID a ser atribuído a uma nova tarefa.
    /// </summary>
    private int _proximoId = 1;

    /// <summary>
    /// Obtém todas as tarefas cadastradas.
    /// </summary>
    public IEnumerable<Tarefa> ObterTodas()
    {
        return _tarefas.AsReadOnly();
    }

    /// <summary>
    /// Obtém uma tarefa pelo seu ID.
    /// </summary>
    public Tarefa? ObterPorId(int id)
    {
        return _tarefas.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Cria uma nova tarefa após validar os dados da requisição.
    /// </summary>
    public Tarefa Criar(CriarTarefaRequest request)
    {
        // Validação: título não pode ser vazio
        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            throw new ArgumentException("O título da tarefa é obrigatório.");
        }

        // Criação da nova tarefa
        var tarefa = new Tarefa
        {
            Id = _proximoId++,
            Titulo = request.Titulo.Trim(),
            Descricao = request.Descricao,
            Concluida = false,
            CriadaEm = DateTime.UtcNow
        };

        // Armazena a tarefa no repositório
        _tarefas.Add(tarefa);
        return tarefa;
    }

    /// <summary>
    /// Atualiza uma tarefa existente com os dados fornecidos.
    /// Permite atualização parcial (apenas os campos informados são alterados).
    /// </summary>
    public Tarefa? Atualizar(int id, AtualizarTarefaRequest request)
    {
        // Busca a tarefa existente
        var tarefa = ObterPorId(id);
        if (tarefa is null)
        {
            return null;
        }

        // Atualiza apenas os campos fornecidos (null significa não atualizar)
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

    /// <summary>
    /// Deleta uma tarefa do repositório.
    /// </summary>
    public Tarefa? Deletar(int id)
    {
        // Busca a tarefa a deletar
        var tarefa = ObterPorId(id);
        if (tarefa is null)
        {
            return null;
        }

        // Remove a tarefa do repositório
        _tarefas.Remove(tarefa);
        return tarefa;
    }
}
