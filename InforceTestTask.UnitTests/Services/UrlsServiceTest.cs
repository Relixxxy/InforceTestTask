using InforceTestTask.Infrastructure.Services.Interfaces;
using InforceTestTask.Infrastructure.Services;
using InforceTestTask.Data.Repositories.Interfaces;
using InforceTestTask.Data.Contexts;
using AutoMapper;
using InforceTestTask.Services;
using InforceTestTask.Services.Interfaces;
using FluentAssertions;
using InforceTestTask.ViewModels;
using InforceTestTask.Data.Entities;

namespace InforceTestTask.UnitTests.Services;

public class UrlsServiceTest
{
    private readonly Mock<IUrlsRepository> _repositoryMock;
    private readonly Mock<IDbContextWrapper<UrlsDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<BaseDataService<UrlsDbContext>>> _baseLoggerMock;
    private readonly Mock<ILogger<UrlsService>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly IUrlsService _urlsService;

    public UrlsServiceTest()
    {
        _repositoryMock = new Mock<IUrlsRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<UrlsDbContext>>();
        _baseLoggerMock = new Mock<ILogger<BaseDataService<UrlsDbContext>>>();
        _loggerMock = new Mock<ILogger<UrlsService>>();
        _mapperMock = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _urlsService = new UrlsService(
            _repositoryMock.Object,
            _dbContextWrapper.Object,
            _baseLoggerMock.Object,
            _loggerMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task AddUrlAsync_Failture()
    {
        int? id = null;

        // Arrange
        _repositoryMock.Setup(x => x.AddUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(id);

        // Act
        var result = await _urlsService.AddUrlAsync("originalUrl", "createdBy");

        // Assert
        result.Should().BeNull();

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {result} was created!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task AddUrlAsync_Success()
    {
        int? id = 1;

        // Arrange
        _repositoryMock.Setup(x => x.AddUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(id);

        // Act
        var result = await _urlsService.AddUrlAsync("originalUrl", "createdBy");

        // Assert
        result.Should().Be(id);

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {result} was created!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task DeleteUrlAsync_Success()
    {
        int id = 1;

        // Arrange
        _repositoryMock.Setup(x => x.DeleteUrlAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // Act
        var result = await _urlsService.DeleteUrlAsync(id);

        // Assert
        result.Should().BeTrue();

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {id} has deleted!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task DeleteUrlAsync_Failture()
    {
        int id = 1;

        // Arrange
        _repositoryMock.Setup(x => x.DeleteUrlAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _urlsService.DeleteUrlAsync(id);

        // Assert
        result.Should().BeFalse();

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Warning,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {id} hasn't deleted!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task GetUrlAsync_Success()
    {
        var id = 1;
        var urlEntity = new ShortUrlEntity { Id = id, OriginalUrl = "https://www.google.com", CreatedBy = "John Doe" };
        var urlVM = new ShortUrlVM { Id = id, OriginalUrl = "https://www.google.com", CreatedBy = "John Doe" };

        // Arrange
        _repositoryMock.Setup(x => x.GetUrlAsync(It.IsAny<int>()))
            .ReturnsAsync(urlEntity);
        _mapperMock.Setup(x => x.Map<ShortUrlVM>(It.IsAny<ShortUrlEntity>()))
            .Returns(urlVM);

        // Act
        var result = await _urlsService.GetUrlAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ShortUrlVM>();
        result.Id.Should().Be(id);

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Information,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {id} has found and mapped to vm!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task GetUrlAsync_Failture()
    {
        var id = 1;
        ShortUrlEntity urlEntity = null!;

        // Arrange
        _repositoryMock.Setup(x => x.GetUrlAsync(It.IsAny<int>()))
            .ReturnsAsync(urlEntity);

        // Act
        var result = await _urlsService.GetUrlAsync(id);

        // Assert
        result.Should().BeNull();

        _loggerMock.Verify(
           x => x.Log(
               LogLevel.Warning,
               It.IsAny<EventId>(),
               It.Is<It.IsAnyType>((o, t) => o.ToString() !
                   .Contains($"Url with id {id} hasn't found!")),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
           Times.Once);
    }

    [Fact]
    public async Task GetUrlsListAsync_Success()
    {
        // Arrange
        var urlEntities = new List<ShortUrlEntity>()
        {
            new ShortUrlEntity() { Id = 1, OriginalUrl = "http://www.google.com", ShortUrl = "abc123" },
            new ShortUrlEntity() { Id = 2, OriginalUrl = "http://www.facebook.com", ShortUrl = "def456" }
        };

        _repositoryMock.Setup(repo => repo.GetUrlsListAsync()).ReturnsAsync(urlEntities);

        // Act
        var result = await _urlsService.GetUrlsListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _loggerMock.Verify(
          x => x.Log(
              LogLevel.Information,
              It.IsAny<EventId>(),
              It.Is<It.IsAnyType>((o, t) => o.ToString() !
                  .Contains($"{urlEntities.Count()} urls has found and mapped to vm!")),
              It.IsAny<Exception>(),
              It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
          Times.Once);
    }
}
