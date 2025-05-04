using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.DTOs;

/// <summary>
/// DTO para exibir os detalhes de uma tarefa.
/// </summary>
public class TarefaDto
{
    /// <summary>
    /// ID único da tarefa.
    /// </summary>
    /// <example>f9e8d7c6-b5a4-3210-fedc-ba9876543210</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Título da tarefa.
    /// </summary>
    /// <example>Enviar relatório</example>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descrição da tarefa.
    /// </summary>
    /// <example>Consolidar os dados e enviar o relatório semanal.</example>
    public string? Descricao { get; set; }

    /// <summary>
    /// Data de vencimento da tarefa.
    /// </summary>
    /// <example>2026-05-07T09:00:00</example>
    public DateTime? DataVencimento { get; set; }

    /// <summary>
    /// Status atual da tarefa.
    /// </summary>
    /// <example>Concluida</example>
    public StatusTarefa Status { get; set; }
}