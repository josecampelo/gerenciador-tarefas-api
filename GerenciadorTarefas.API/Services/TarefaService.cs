using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.API.Domain;
using GerenciadorTarefas.API.Data;

namespace GerenciadorTarefas.API.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;

    public TarefaService(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

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

        var tarefaAdicionada = _tarefaRepository.Adicionar(novaTarefa);
        return ConverterParaDto(tarefaAdicionada);
    }

    public IEnumerable<TarefaDto> ListarTarefas(string? status, DateTime? dataVencimento)
    {
        var tarefas = _tarefaRepository.ObterTodos();
        var query = tarefas.AsQueryable();

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
        var tarefa = _tarefaRepository.ObterPorId(id);
        return tarefa != null ? ConverterParaDto(tarefa) : null;
    }

    public TarefaDto? AtualizarTarefa(AtualizarTarefaDto atualizarTarefaDto)
    {
        var tarefaExistente = _tarefaRepository.ObterPorId(atualizarTarefaDto.Id);

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

        var tarefaAtualizada = _tarefaRepository.Atualizar(tarefaExistente);
        return ConverterParaDto(tarefaAtualizada);
    }

    public TarefaDto? ExcluirTarefa(Guid id)
    {
        var tarefaParaRemover = _tarefaRepository.ObterPorId(id);
        if (tarefaParaRemover != null)
        {
            var tarefaRemovida = _tarefaRepository.Remover(tarefaParaRemover);
            return tarefaRemovida != null ? ConverterParaDto(tarefaRemovida) : null;
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