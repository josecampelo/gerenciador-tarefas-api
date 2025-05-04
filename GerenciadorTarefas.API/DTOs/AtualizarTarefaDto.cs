using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.DTOs;

public class AtualizarTarefaDto
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataVencimento { get; set; }
    public StatusTarefa? Status { get; set; }
}