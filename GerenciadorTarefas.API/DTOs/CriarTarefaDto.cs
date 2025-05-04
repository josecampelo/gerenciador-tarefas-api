using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.DTOs;

/// <summary>
/// DTO para criar uma nova tarefa.
/// </summary>
public class CriarTarefaDto
{
    /// <summary>
    /// Título da tarefa (obrigatório).
    /// </summary>
    /// <example>Fazer compras</example>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da tarefa (opcional).
    /// </summary>
    /// <example>Comprar leite, ovos e pão.</example>
    public string? Descricao { get; set; }

    /// <summary>
    /// Data de vencimento da tarefa (opcional).
    /// </summary>
    /// <example>2026-05-05T10:00:00</example>
    public DateTime? DataVencimento { get; set; }

    /// <summary>
    /// Status da tarefa (Pendente, EmProgresso, Concluida).
    /// </summary>
    /// <example>Pendente</example>
    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
}