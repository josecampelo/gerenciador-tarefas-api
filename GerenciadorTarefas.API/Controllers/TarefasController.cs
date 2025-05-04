using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar as tarefas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    /// <summary>
    /// Construtor do TarefasController.
    /// </summary>
    /// <param name="tarefaService">Serviço de tarefas injetado.</param>
    public TarefasController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="criarTarefaDto">Dados para criar a tarefa.</param>
    /// <returns>A tarefa recém-criada.</returns>
    /// <response code="201">Tarefa criada com sucesso.</response>
    /// <response code="400">Dados de requisição inválidos.</response>
    /// <response code="500">Erro interno ao criar a tarefa.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TarefaDto> CriarTarefa([FromBody] CriarTarefaDto criarTarefaDto)
    {
        try
        {
            var tarefaCriada = _tarefaService.CriarTarefa(criarTarefaDto);
            return CreatedAtAction(nameof(ObterTarefaPorId), new { id = tarefaCriada.Id }, tarefaCriada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar tarefa: {ex.Message}"); // Logging
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao criar a tarefa.");
        }
    }

    /// <summary>
    /// Lista todas as tarefas, com opção de filtrar por status e/ou data de vencimento.
    /// </summary>
    /// <param name="status">Filtro opcional por status da tarefa (Pendente, EmProgresso, Concluida).</param>
    /// <param name="dataVencimento">Filtro opcional por data de vencimento da tarefa (no formato YYYY-MM-DD).</param>
    /// <returns>Uma coleção de tarefas.</returns>
    /// <response code="200">Lista de tarefas retornada com sucesso.</response>
    /// <response code="500">Erro interno ao listar as tarefas.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TarefaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<TarefaDto>> ListarTarefas([FromQuery] string? status, [FromQuery] DateTime? dataVencimento)
    {
        try
        {
            var tarefas = _tarefaService.ListarTarefas(status, dataVencimento);
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao listar tarefas: {ex.Message}"); // Logging
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao listar as tarefas.");
        }
    }

    /// <summary>
    /// Obtém uma tarefa específica por seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser buscada.</param>
    /// <returns>A tarefa encontrada.</returns>
    /// <response code="200">Tarefa encontrada com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    /// <response code="500">Erro interno ao obter a tarefa.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TarefaDto> ObterTarefaPorId(Guid id)
    {
        try
        {
            var tarefa = _tarefaService.ObterTarefaPorId(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter tarefa por ID {id}: {ex.Message}"); // Logging
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter a tarefa.");
        }
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser atualizada.</param>
    /// <param name="atualizarTarefaDto">Dados para atualizar a tarefa.</param>
    /// <returns>A tarefa atualizada.</returns>
    /// <response code="200">Tarefa atualizada com sucesso.</response>
    /// <response code="400">Dados de requisição inválidos.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    /// <response code="500">Erro interno ao atualizar a tarefa.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TarefaDto> AtualizarTarefa(Guid id, [FromBody] AtualizarTarefaDto atualizarTarefaDto)
    {
        if (id != atualizarTarefaDto.Id)
        {
            return BadRequest("O ID da tarefa na rota não corresponde ao ID no corpo da requisição.");
        }

        try
        {
            var tarefaAtualizada = _tarefaService.AtualizarTarefa(atualizarTarefaDto);
            if (tarefaAtualizada == null)
            {
                return NotFound();
            }
            return Ok(tarefaAtualizada);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao atualizar tarefa {id}: {ex.Message}"); // Logging
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar a tarefa.");
        }
    }

    /// <summary>
    /// Exclui uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser excluída.</param>
    /// <returns>A tarefa que foi excluída.</returns>
    /// <response code="200">Tarefa excluída com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    /// <response code="500">Erro interno ao excluir a tarefa.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TarefaDto> ExcluirTarefa(Guid id)
    {
        try
        {
            var tarefaExcluida = _tarefaService.ExcluirTarefa(id);
            if (tarefaExcluida == null)
            {
                return NotFound();
            }
            return Ok(tarefaExcluida);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao excluir tarefa {id}: {ex.Message}"); // Logging
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao excluir a tarefa.");
        }
    }
}