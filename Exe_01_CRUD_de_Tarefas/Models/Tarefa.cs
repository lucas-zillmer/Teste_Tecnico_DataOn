/// <summary>
/// Modelo de domínio que representa uma tarefa no sistema.
/// </summary>
namespace Exe_01_CRUD_de_Tarefas.Models;

public class Tarefa
{
    /// <summary>
    /// Identificador único da tarefa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título ou nome da tarefa. Campo obrigatório.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da tarefa. Campo opcional.
    /// </summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// Indica se a tarefa foi concluída.
    /// </summary>
    public bool Concluida { get; set; }

    /// <summary>
    /// Data e hora em que a tarefa foi criada (em UTC).
    /// </summary>
    public DateTime CriadaEm { get; set; }
}
