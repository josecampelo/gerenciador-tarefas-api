namespace GerenciadorTarefas.API.Domain;

public class Tarefa
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime? DataVencimento { get; set; }

    public StatusTarefa Status { get; set; }
}

public enum StatusTarefa
{
    Pendente,
    EmProgresso,
    Concluida
}