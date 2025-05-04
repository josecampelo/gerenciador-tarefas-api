using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.DTOs;

public class CriarTarefaDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public DateTime? DataVencimento { get; set; }
    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
}