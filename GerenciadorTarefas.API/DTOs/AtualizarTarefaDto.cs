using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.DTOs;

/// <summary>
/// DTO para atualizar uma tarefa existente.
/// </summary>
public class AtualizarTarefaDto
{
    /// <summary>
    /// ID da tarefa a ser atualizada (obrigatório).
    /// </summary>
    /// <example>a1b2c3d4-e5f6-7890-1234-567890abcdef</example>
    public Guid Id { get; set; }

    /// <summary>
    /// Novo título da tarefa (opcional).
    /// </summary>
    /// <example>Ligar para o cliente</example>
    public string? Titulo { get; set; }

    /// <summary>
    /// Nova descrição da tarefa (opcional).
    /// </summary>
    /// <example>Discutir os requisitos do projeto.</example>
    public string? Descricao { get; set; }

    /// <summary>
    /// Nova data de vencimento da tarefa (opcional).
    /// </summary>
    /// <example>2026-05-06T15:00:00</example>
    public DateTime? DataVencimento { get; set; }

    /// <summary>
    /// Novo status da tarefa (opcional).
    /// </summary>
    /// <example>EmProgresso</example>
    public StatusTarefa? Status { get; set; }
}