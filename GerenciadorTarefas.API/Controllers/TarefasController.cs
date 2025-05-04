using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefasController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao criar a tarefa.");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TarefaDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<TarefaDto>> ListarTarefas([FromQuery] string? status, [FromQuery] DateTime? dataVencimento)
    {
        var tarefas = _tarefaService.ListarTarefas(status, dataVencimento);
        return Ok(tarefas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TarefaDto> ObterTarefaPorId(Guid id)
    {
        var tarefa = _tarefaService.ObterTarefaPorId(id);
        if (tarefa == null)
        {
            return NotFound();
        }
        return Ok(tarefa);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao atualizar a tarefa.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(TarefaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<TarefaDto> ExcluirTarefa(Guid id)
    {
        var tarefaExcluida = _tarefaService.ExcluirTarefa(id);
        if (tarefaExcluida == null)
        {
            return NotFound();
        }
        return Ok(tarefaExcluida);
    }
}