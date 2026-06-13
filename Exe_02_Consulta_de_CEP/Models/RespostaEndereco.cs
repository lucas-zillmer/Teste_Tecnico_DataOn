/// <summary>
/// DTO (Data Transfer Object) que representa um endereço retornado pela API.
/// Formato normalizado para resposta ao cliente.
/// </summary>
namespace Exe_02_Consulta_de_CEP.Models;

public sealed class RespostaEndereco
{
    /// <summary>
    /// CEP consultado.
    /// </summary>
    public string? Cep { get; set; }

    /// <summary>
    /// Logradouro (rua, avenida, etc).
    /// </summary>
    public string? Logradouro { get; set; }

    /// <summary>
    /// Bairro do endereço.
    /// </summary>
    public string? Bairro { get; set; }

    /// <summary>
    /// Cidade ou localidade.
    /// </summary>
    public string? Cidade { get; set; }

    /// <summary>
    /// Sigla do estado.
    /// </summary>
    public string? Estado { get; set; }
}
