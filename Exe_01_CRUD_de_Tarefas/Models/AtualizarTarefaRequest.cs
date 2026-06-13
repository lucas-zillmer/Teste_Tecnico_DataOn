/// <summary>
/// DTO (Data Transfer Object) para requisição de atualização de tarefa.
/// Permite atualizar parcialmente os campos da tarefa.
/// </summary>
namespace Exe_01_CRUD_de_Tarefas.Models;

public class AtualizarTarefaRequest
{
    /// <summary>
    /// Novo título da tarefa. Se fornecido, atualiza o título.
    /// </summary>
    public string? Titulo { get; set; }

    /// <summary>
    /// Nova descrição da tarefa. Se fornecida, atualiza a descrição.
    /// </summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// Status de conclusão da tarefa. Se fornecido, atualiza o status.
    /// </summary>
    public bool? Concluida { get; set; }
}
