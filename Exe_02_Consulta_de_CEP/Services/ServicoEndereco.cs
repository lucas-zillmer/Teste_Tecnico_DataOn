using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using Exe_02_Consulta_de_CEP.Models;

/// <summary>
/// Serviço responsável pela lógica de consulta de endereços via CEP.
/// Integra-se com a API externa ViaCep para obter informações de endereços.
/// Utiliza HttpClientFactory para gerenciar conexões HTTP de forma eficiente e segura.
/// Inclui validação de formato de CEP e tratamento de erros para garantir robustez na aplicação.
/// 
/// A implementação segue o padrão de injeção de dependências, permitindo fácil teste e manutenção.
/// O serviço é registrado como singleton na configuração da aplicação, garantindo que uma única instância seja utilizada durante toda a execução.
/// O método principal, ConsultarPorCepAsync, realiza a consulta e mapeia os dados para o formato de resposta esperado pela API.
/// A validação do formato do CEP é feita utilizando uma expressão regular, garantindo que apenas CEPs válidos sejam processados.
/// O serviço também inclui logging para rastreamento de operações e erros, facilitando a identificação de problemas em produção.
/// 
/// Exemplo de uso:
/// var endereco = await servicoEndereco.ConsultarPorCepAsync("01001000");
/// </summary>

namespace Exe_02_Consulta_de_CEP.Services;

public interface IServicoEndereco
{
    /// <summary>
    /// Consulta um endereço através do CEP fornecido.
    /// </summary>
    /// <param name="cep">O CEP a ser consultado (sem formatação, apenas 8 dígitos).</param>
    /// <returns>Os dados do endereço se encontrado, ou null se não encontrado.</returns>
    Task<RespostaEndereco?> ConsultarPorCepAsync(string cep);
}

/// <summary>
/// Implementação do serviço de consulta de endereços.
/// Utiliza HttpClientFactory para gerenciar conexões HTTP de forma eficiente.
/// </summary>
public class ServicoEndereco : IServicoEndereco
{
    /// <summary>
    /// Factory para criar instâncias do HttpClient com configurações pré-definidas.
    /// </summary>
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Logger para registrar informações e erros de operações.
    /// </summary>
    private readonly ILogger<ServicoEndereco> _logger;

    public ServicoEndereco(IHttpClientFactory httpClientFactory, ILogger<ServicoEndereco> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <summary>
    /// Consulta um endereço através do CEP fornecido na API ViaCep.
    /// </summary>
    public async Task<RespostaEndereco?> ConsultarPorCepAsync(string cep)
    {
        // Valida o formato do CEP: deve ter exatamente 8 dígitos numéricos
        if (!ValidarFormatoCep(cep))
        {
            _logger.LogWarning($"CEP com formato inválido: {cep}");
            throw new ArgumentException("CEP inválido. Informe exatamente 8 dígitos numéricos.");
        }

        try
        {
            // Obtém o HttpClient configurado para ViaCep
            var client = _httpClientFactory.CreateClient("ViaCep");

            // Realiza a requisição para a API ViaCep
            _logger.LogInformation($"Consultando CEP: {cep}");
            var resposta = await client.GetAsync($"ws/{cep}/json/");

            // Verifica se a requisição foi bem-sucedida
            if (!resposta.IsSuccessStatusCode)
            {
                _logger.LogError($"Erro ao consultar ViaCep. Status: {resposta.StatusCode}");
                return null;
            }

            // Desserializa a resposta JSON para o modelo ViaCep
            var viaCepResposta = await resposta.Content.ReadFromJsonAsync<RespostaViaCep>();

            // Verifica se houve erro ou se o CEP não foi encontrado
            if (viaCepResposta is null || viaCepResposta.Erro)
            {
                _logger.LogWarning($"CEP não encontrado: {cep}");
                return null;
            }

            // Mapeia a resposta do ViaCep para o DTO de resposta da API
            var endereco = new RespostaEndereco
            {
                Cep = viaCepResposta.Cep,
                Logradouro = viaCepResposta.Logradouro,
                Bairro = viaCepResposta.Bairro,
                Cidade = viaCepResposta.Localidade,
                Estado = viaCepResposta.Uf
            };

            _logger.LogInformation($"Endereço encontrado para CEP: {cep}");
            return endereco;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão ao consultar ViaCep: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError($"Erro ao desserializar resposta do ViaCep: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Valida se o CEP possui o formato correto (8 dígitos numéricos).
    /// </summary>
    private static bool ValidarFormatoCep(string cep)
    {
        // Regex que valida exatamente 8 dígitos
        return !string.IsNullOrEmpty(cep) && Regex.IsMatch(cep, @"^\d{8}$");
    }
}
