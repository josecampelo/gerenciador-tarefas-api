using GerenciadorTarefas.API.DTOs;
using GerenciadorTarefas.API.Domain;
using GerenciadorTarefas.API.Services;
using GerenciadorTarefas.API.Data;
using Moq;

public class TarefaServiceTests
{
    private readonly Mock<ITarefaRepository> _mockTarefaRepository;
    private readonly TarefaService _tarefaService;

    public TarefaServiceTests()
    {
        _mockTarefaRepository = new Mock<ITarefaRepository>();
        _tarefaService = new TarefaService(_mockTarefaRepository.Object);
    }

    [Fact]
    public void CriarTarefa_TituloValido_RetornaTarefaCriada()
    {
        // Arrange
        var criarTarefaDto = new CriarTarefaDto { Titulo = "Nova Tarefa", Descricao = "Descrição", DataVencimento = DateTime.Now.AddDays(1), Status = StatusTarefa.Pendente };
        var tarefaEsperada = new Tarefa { Id = Guid.NewGuid(), Titulo = criarTarefaDto.Titulo, Descricao = criarTarefaDto.Descricao, DataVencimento = criarTarefaDto.DataVencimento, Status = criarTarefaDto.Status };

        _mockTarefaRepository.Setup(repo => repo.Adicionar(It.IsAny<Tarefa>())).Returns(tarefaEsperada);

        // Act
        var resultado = _tarefaService.CriarTarefa(criarTarefaDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(criarTarefaDto.Titulo, resultado.Titulo);
        _mockTarefaRepository.Verify(repo => repo.Adicionar(It.IsAny<Tarefa>()), Times.Once);
    }

    [Fact]
    public void CriarTarefa_TituloInvalido_LancaArgumentException()
    {
        // Arrange
        var criarTarefaDto = new CriarTarefaDto { Titulo = "", Descricao = "Descrição", DataVencimento = DateTime.Now.AddDays(1), Status = StatusTarefa.Pendente };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _tarefaService.CriarTarefa(criarTarefaDto));
        _mockTarefaRepository.Verify(repo => repo.Adicionar(It.IsAny<Tarefa>()), Times.Never);
    }

    [Fact]
    public void ListarTarefas_SemFiltro_RetornaTodasAsTarefas()
    {
        // Arrange
        var tarefas = new List<Tarefa> {
        new Tarefa { Id = Guid.NewGuid(), Titulo = "Tarefa 1", Status = StatusTarefa.Pendente },
        new Tarefa { Id = Guid.NewGuid(), Titulo = "Tarefa 2", Status = StatusTarefa.Concluida }
    };
        _mockTarefaRepository.Setup(repo => repo.ObterTodos()).Returns(tarefas);

        // Act
        var resultado = _tarefaService.ListarTarefas(null, null);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
        _mockTarefaRepository.Verify(repo => repo.ObterTodos(), Times.Once);
    }

    [Fact]
    public void ObterTarefaPorId_TarefaExistente_RetornaTarefaDto()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        var tarefa = new Tarefa { Id = tarefaId, Titulo = "Tarefa Existente", Status = StatusTarefa.Pendente };
        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns(tarefa);

        // Act
        var resultado = _tarefaService.ObterTarefaPorId(tarefaId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(tarefaId, resultado.Id);
        Assert.Equal(tarefa.Titulo, resultado.Titulo);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
    }

    [Fact]
    public void ObterTarefaPorId_TarefaNaoExistente_RetornaNull()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns((Tarefa)null);

        // Act
        var resultado = _tarefaService.ObterTarefaPorId(tarefaId);

        // Assert
        Assert.Null(resultado);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
    }

    [Fact]
    public void AtualizarTarefa_TarefaExistente_RetornaTarefaAtualizada()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        var tarefaExistente = new Tarefa { Id = tarefaId, Titulo = "Tarefa Original", Descricao = "Descricao Original", Status = StatusTarefa.Pendente };
        var atualizarTarefaDto = new AtualizarTarefaDto { Id = tarefaId, Titulo = "Tarefa Atualizada", Status = StatusTarefa.Concluida };
        var tarefaAtualizada = new Tarefa { Id = tarefaId, Titulo = "Tarefa Atualizada", Descricao = "Descricao Original", Status = StatusTarefa.Concluida };

        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns(tarefaExistente);
        _mockTarefaRepository.Setup(repo => repo.Atualizar(It.IsAny<Tarefa>())).Returns(tarefaAtualizada);

        // Act
        var resultado = _tarefaService.AtualizarTarefa(atualizarTarefaDto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(tarefaId, resultado.Id);
        Assert.Equal(atualizarTarefaDto.Titulo, resultado.Titulo);
        Assert.Equal(atualizarTarefaDto.Status, resultado.Status);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
        _mockTarefaRepository.Verify(repo => repo.Atualizar(It.IsAny<Tarefa>()), Times.Once);
    }

    [Fact]
    public void AtualizarTarefa_TarefaNaoExistente_RetornaNull()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        var atualizarTarefaDto = new AtualizarTarefaDto { Id = tarefaId, Titulo = "Tarefa Atualizada", Status = StatusTarefa.Concluida };
        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns((Tarefa)null);

        // Act
        var resultado = _tarefaService.AtualizarTarefa(atualizarTarefaDto);

        // Assert
        Assert.Null(resultado);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
        _mockTarefaRepository.Verify(repo => repo.Atualizar(It.IsAny<Tarefa>()), Times.Never);
    }

    [Fact]
    public void AtualizarTarefa_DataVencimentoNoPassado_LancaArgumentException()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        var tarefaExistente = new Tarefa { Id = tarefaId, Titulo = "Tarefa Original", Status = StatusTarefa.Pendente };
        var atualizarTarefaDto = new AtualizarTarefaDto { Id = tarefaId, DataVencimento = DateTime.Now.AddDays(-1), Status = StatusTarefa.Concluida };

        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns(tarefaExistente);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _tarefaService.AtualizarTarefa(atualizarTarefaDto));
        _mockTarefaRepository.Verify(repo => repo.Atualizar(It.IsAny<Tarefa>()), Times.Never);
    }

    [Fact]
    public void ExcluirTarefa_TarefaExistente_RetornaTarefaExcluida()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        var tarefaParaRemover = new Tarefa { Id = tarefaId, Titulo = "Tarefa a Excluir", Status = StatusTarefa.Pendente };

        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns(tarefaParaRemover);
        _mockTarefaRepository.Setup(repo => repo.Remover(tarefaParaRemover)).Returns(tarefaParaRemover);

        // Act
        var resultado = _tarefaService.ExcluirTarefa(tarefaId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(tarefaId, resultado.Id);
        Assert.Equal(tarefaParaRemover.Titulo, resultado.Titulo);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
        _mockTarefaRepository.Verify(repo => repo.Remover(tarefaParaRemover), Times.Once);
    }

    [Fact]
    public void ExcluirTarefa_TarefaNaoExistente_RetornaNull()
    {
        // Arrange
        var tarefaId = Guid.NewGuid();
        _mockTarefaRepository.Setup(repo => repo.ObterPorId(tarefaId)).Returns((Tarefa)null);

        // Act
        var resultado = _tarefaService.ExcluirTarefa(tarefaId);

        // Assert
        Assert.Null(resultado);
        _mockTarefaRepository.Verify(repo => repo.ObterPorId(tarefaId), Times.Once);
        _mockTarefaRepository.Verify(repo => repo.Remover(It.IsAny<Tarefa>()), Times.Never);
    }
}