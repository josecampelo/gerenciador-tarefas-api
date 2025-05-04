using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.API.Domain;

namespace GerenciadorTarefas.API.Services;

public class TarefaService : ITarefaService
{
    private readonly List<Tarefa> _tarefas = new List<Tarefa>();

    public TarefaDto CriarTarefa(CriarTarefaDto criarTarefaDto)
    {
        if (string.IsNullOrWhiteSpace(criarTarefaDto.Titulo))
        {
            throw new ArgumentException("O título da tarefa é obrigatório.", nameof(criarTarefaDto.Titulo));
        }

        if (criarTarefaDto.DataVencimento.HasValue && criarTarefaDto.DataVencimento.Value.Date <= DateTime.Now.Date)
        {
            throw new ArgumentException("A data de vencimento deve ser no futuro.", nameof(criarTarefaDto.DataVencimento));
        }

        var novaTarefa = new Tarefa
        {
            Id = Guid.NewGuid(),
            Titulo = criarTarefaDto.Titulo,
            Descricao = criarTarefaDto.Descricao,
            DataVencimento = criarTarefaDto.DataVencimento,
            Status = criarTarefaDto.Status
        };

        _tarefas.Add(novaTarefa);

        return ConverterParaDto(novaTarefa);
    }

    public IEnumerable<TarefaDto> ListarTarefas(string? status, DateTime? dataVencimento)
    {
        var query = _tarefas.AsQueryable();

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<StatusTarefa>(status, true, out var statusFiltro))
        {
            query = query.Where(t => t.Status == statusFiltro);
        }

        if (dataVencimento.HasValue)
        {
            query = query.Where(t => t.DataVencimento.HasValue && t.DataVencimento.Value.Date == dataVencimento.Value.Date);
        }

        return query.Select(ConverterParaDto).ToList();
    }

    public TarefaDto? ObterTarefaPorId(Guid id)
    {
        var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);

        return tarefa != null ? ConverterParaDto(tarefa) : null;
    }

    public TarefaDto? AtualizarTarefa(AtualizarTarefaDto atualizarTarefaDto)
    {
        var tarefaExistente = _tarefas.FirstOrDefault(t => t.Id == atualizarTarefaDto.Id);

        if (tarefaExistente == null)
        {
            return null;
        }

        if (atualizarTarefaDto.Titulo != null)
        {
            tarefaExistente.Titulo = atualizarTarefaDto.Titulo;
        }
        if (atualizarTarefaDto.Descricao != null)
        {
            tarefaExistente.Descricao = atualizarTarefaDto.Descricao;
        }
        if (atualizarTarefaDto.DataVencimento.HasValue)
        {
            if (atualizarTarefaDto.DataVencimento.Value.Date <= DateTime.Now.Date)
            {
                throw new ArgumentException("A data de vencimento deve ser no futuro.", nameof(atualizarTarefaDto.DataVencimento));
            }

            tarefaExistente.DataVencimento = atualizarTarefaDto.DataVencimento;
        }
        if (atualizarTarefaDto.Status.HasValue)
        {
            tarefaExistente.Status = atualizarTarefaDto.Status.Value;
        }

        return ConverterParaDto(tarefaExistente);
    }

    public TarefaDto? ExcluirTarefa(Guid id)
    {
        var tarefaParaRemover = _tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefaParaRemover != null)
        {
            _tarefas.Remove(tarefaParaRemover);

            return ConverterParaDto(tarefaParaRemover);
        }

        return null;
    }

    private static TarefaDto ConverterParaDto(Tarefa tarefa)
    {
        return new TarefaDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            DataVencimento = tarefa.DataVencimento,
            Status = tarefa.Status
        };
    }
}