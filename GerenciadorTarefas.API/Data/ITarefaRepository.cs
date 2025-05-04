using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.Data;

/// <summary>
/// Interface que define as operações de acesso aos dados para a entidade Tarefa.
/// </summary>
public interface ITarefaRepository
{
    /// <summary>
    /// Adiciona uma nova tarefa ao repositório.
    /// </summary>
    /// <param name="tarefa">A tarefa a ser adicionada.</param>
    /// <returns>A tarefa adicionada.</returns>
    Tarefa Adicionar(Tarefa tarefa);

    /// <summary>
    /// Obtém todas as tarefas do repositório.
    /// </summary>
    /// <returns>Uma coleção de tarefas.</returns>
    IEnumerable<Tarefa> ObterTodos();

    /// <summary>
    /// Obtém uma tarefa pelo seu ID.
    /// </summary>
    /// <param name="id">O ID da tarefa.</param>
    /// <returns>A tarefa, ou null se não encontrada.</returns>
    Tarefa? ObterPorId(Guid id);

    /// <summary>
    /// Atualiza uma tarefa existente no repositório.
    /// </summary>
    /// <param name="tarefa">A tarefa com os dados atualizados.</param>
    /// <returns>A tarefa atualizada.</returns>
    Tarefa Atualizar(Tarefa tarefa);

    /// <summary>
    /// Remove uma tarefa do repositório.
    /// </summary>
    /// <param name="tarefa">A tarefa a ser removida.</param>
    /// <returns>A tarefa que foi removida.</returns>
    Tarefa? Remover(Tarefa tarefa);
}