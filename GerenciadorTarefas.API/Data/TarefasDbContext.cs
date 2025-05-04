using GerenciadorTarefas.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.API.Data;

public class TarefasDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("GerenciadorTarefasDb");
    }
}