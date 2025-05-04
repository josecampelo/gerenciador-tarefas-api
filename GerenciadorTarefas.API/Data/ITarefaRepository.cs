using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.Data;

public interface ITarefaRepository
{
    Tarefa Adicionar(Tarefa tarefa);
    IEnumerable<Tarefa> ObterTodos();
    Tarefa? ObterPorId(Guid id);
    Tarefa Atualizar(Tarefa tarefa);
    Tarefa? Remover(Tarefa tarefa);
}