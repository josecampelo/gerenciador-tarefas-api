using GerenciadorTarefas.API.DTOs;

namespace GerenciadorTarefas.API.Services;

public interface ITarefaService
{
    TarefaDto CriarTarefa(CriarTarefaDto criarTarefaDto);
    IEnumerable<TarefaDto> ListarTarefas(string? status, DateTime? dataVencimento);
    TarefaDto? ObterTarefaPorId(Guid id);
    TarefaDto? AtualizarTarefa(AtualizarTarefaDto atualizarTarefaDto);
    TarefaDto? ExcluirTarefa(Guid id);
}