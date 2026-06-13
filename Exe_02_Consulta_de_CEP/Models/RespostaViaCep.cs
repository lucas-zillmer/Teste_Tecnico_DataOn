using System.Text.Json.Serialization;

/// <summary>
/// Modelo que representa a resposta da API ViaCep.
/// Mapeia os dados retornados pelo serviço externo de consulta de CEP.
/// </summary>
namespace Exe_02_Consulta_de_CEP.Models;

public sealed class RespostaViaCep
{
    /// <summary>
    /// CEP no formato com hífen (ex: 01234-567).
    /// </summary>
    [JsonPropertyName("cep")]
    public string? Cep { get; set; }

    /// <summary>
    /// Nome da rua ou avenida.
    /// </summary>
    [JsonPropertyName("logradouro")]
    public string? Logradouro { get; set; }

    /// <summary>
    /// Número do complemento do endereço (ex: apto, sala).
    /// </summary>
    [JsonPropertyName("complemento")]
    public string? Complemento { get; set; }

    /// <summary>
    /// Nome do bairro.
    /// </summary>
    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }

    /// <summary>
    /// Nome da cidade/localidade.
    /// </summary>
    [JsonPropertyName("localidade")]
    public string? Localidade { get; set; }

    /// <summary>
    /// Sigla do estado (ex: SP, RJ).
    /// </summary>
    [JsonPropertyName("uf")]
    public string? Uf { get; set; }

    /// <summary>
    /// Código IBGE do município.
    /// </summary>
    [JsonPropertyName("ibge")]
    public string? Ibge { get; set; }

    /// <summary>
    /// Indicador de erro. True se o CEP não foi encontrado.
    /// </summary>
    [JsonPropertyName("erro")]
    public bool Erro { get; set; }
}
