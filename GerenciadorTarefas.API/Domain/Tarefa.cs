namespace GerenciadorTarefas.API.Domain;

public class Tarefa
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string? Descricao { get; set; }

    public DateTime? DataVencimento { get; set; }

    public StatusTarefa Status { get; set; }
}

/// <summary>
/// Enum que representa o status de uma tarefa.
/// </summary>
public enum StatusTarefa
{
    /// <summary>
    /// A tarefa ainda não foi iniciada.
    /// </summary>
    Pendente,
    /// <summary>
    /// A tarefa está em andamento.
    /// </summary>
    EmProgresso,
    /// <summary>
    /// A tarefa foi concluída.
    /// </summary>
    Concluida
}