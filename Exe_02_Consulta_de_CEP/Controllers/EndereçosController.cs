using Microsoft.AspNetCore.Mvc;
using Exe_02_Consulta_de_CEP.Models;
using Exe_02_Consulta_de_CEP.Services;

namespace Exe_02_Consulta_de_CEP.Controllers;

/// <summary>
/// Controller responsável pelos endpoints da API de consulta de CEP.
/// Fornece operações para consultar endereços através do CEP (Código de Endereçamento Postal).
/// </summary>
[ApiController]
[Route("api/endereco")]
[Produces("application/json")]
public class EndereçosController : ControllerBase
{
    /// <summary>
    /// Serviço injetado para operações de consulta de endereço.
    /// </summary>
    private readonly IServicoEndereco _servicoEndereco;

    /// <summary>
    /// Logger para rastreamento de operações.
    /// </summary>
    private readonly ILogger<EndereçosController> _logger;

    public EndereçosController(IServicoEndereco servicoEndereco, ILogger<EndereçosController> logger)
    {
        _servicoEndereco = servicoEndereco;
        _logger = logger;
    }

    /// <summary>
    /// Consulta um endereço através do CEP fornecido.
    /// </summary>
    /// <param name="cep">O CEP a ser consultado (8 dígitos numéricos).</param>
    /// <returns>Os dados do endereço se encontrado.</returns>
    /// <response code="200">Endereço encontrado e retornado.</response>
    /// <response code="400">CEP com formato inválido.</response>
    /// <response code="404">CEP não encontrado.</response>
    /// <response code="503">Serviço externo indisponível.</response>
    [HttpGet("{cep}")]
    [ProducesResponseType(typeof(RespostaEndereco), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<RespostaEndereco>> ConsultarPorCep(string cep)
    {
        _logger.LogInformation($"Solicitação para consultar CEP: {cep}");

        try
        {
            // Chama o serviço para consultar o endereço
            var endereco = await _servicoEndereco.ConsultarPorCepAsync(cep);

            // Verifica se o endereço foi encontrado
            if (endereco is null)
            {
                _logger.LogWarning($"CEP não encontrado: {cep}");
                return NotFound(new
                {
                    erro = true,
                    mensagem = "CEP não encontrado.",
                    cep = cep
                });
            }

            _logger.LogInformation($"Endereço encontrado para CEP: {cep}");
            return Ok(endereco);
        }
        catch (ArgumentException ex)
        {
            // Erro de validação de formato do CEP
            _logger.LogWarning($"CEP com formato inválido: {cep}. Erro: {ex.Message}");
            return BadRequest(new
            {
                erro = true,
                mensagem = ex.Message
            });
        }
        catch (HttpRequestException ex)
        {
            // Erro de comunicação com o serviço externo
            _logger.LogError($"Erro ao consultar ViaCep para CEP {cep}: {ex.Message}");
            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        catch (Exception ex)
        {
            // Erro genérico
            _logger.LogError($"Erro inesperado ao consultar CEP {cep}: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
