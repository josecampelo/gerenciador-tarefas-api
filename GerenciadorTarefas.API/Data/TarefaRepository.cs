using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.Data;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefasDbContext _dbContext;

    public TarefaRepository(TarefasDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Tarefa Adicionar(Tarefa tarefa)
    {
        _dbContext.Tarefas.Add(tarefa);
        _dbContext.SaveChanges();
        return tarefa;
    }

    public IEnumerable<Tarefa> ObterTodos()
    {
        return _dbContext.Tarefas.ToList();
    }

    public Tarefa? ObterPorId(Guid id)
    {
        return _dbContext.Tarefas.FirstOrDefault(t => t.Id == id);
    }

    public Tarefa Atualizar(Tarefa tarefa)
    {
        _dbContext.Tarefas.Update(tarefa);
        _dbContext.SaveChanges();
        return tarefa;
    }

    public Tarefa? Remover(Tarefa tarefa)
    {
        _dbContext.Tarefas.Remove(tarefa);
        _dbContext.SaveChanges();
        return tarefa;
    }
}