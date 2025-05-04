using GerenciadorTarefas.API.DTOs;

namespace GerenciadorTarefas.API.Services;

/// <summary>
/// Interface que define as operações de negócio para a entidade Tarefa.
/// </summary>
public interface ITarefaService
{
    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="criarTarefaDto">Dados para criar a tarefa.</param>
    /// <returns>O DTO da tarefa criada.</returns>
    TarefaDto CriarTarefa(CriarTarefaDto criarTarefaDto);

    /// <summary>
    /// Lista todas as tarefas, com opção de filtrar por status e/ou data de vencimento.
    /// </summary>
    /// <param name="status">Filtro opcional por status.</param>
    /// <param name="dataVencimento">Filtro opcional por data de vencimento.</param>
    /// <returns>Uma coleção de DTOs de tarefas.</returns>
    IEnumerable<TarefaDto> ListarTarefas(string? status, DateTime? dataVencimento);

    /// <summary>
    /// Obtém uma tarefa específica pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa.</param>
    /// <returns>O DTO da tarefa, ou null se não encontrada.</returns>
    TarefaDto? ObterTarefaPorId(Guid id);

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="atualizarTarefaDto">Dados para atualizar a tarefa.</param>
    /// <returns>O DTO da tarefa atualizada, ou null se não encontrada.</returns>
    TarefaDto? AtualizarTarefa(AtualizarTarefaDto atualizarTarefaDto);

    /// <summary>
    /// Exclui uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa a ser excluída.</param>
    /// <returns>O DTO da tarefa excluída, ou null se não encontrada.</returns>
    TarefaDto? ExcluirTarefa(Guid id);
}