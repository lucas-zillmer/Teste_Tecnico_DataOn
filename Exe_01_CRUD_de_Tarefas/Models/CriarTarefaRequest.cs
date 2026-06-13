/// <summary>
/// DTO (Data Transfer Object) para requisição de criação de tarefa.
/// Recebida do cliente e validada antes da criação.
/// </summary>
namespace Exe_01_CRUD_de_Tarefas.Models;

public class CriarTarefaRequest
{
    /// <summary>
    /// Título da tarefa. Campo obrigatório e não pode ser vazio.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descrição opcional da tarefa.
    /// </summary>
    public string? Descricao { get; set; }
}
