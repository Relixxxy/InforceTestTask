using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InforceTestTask.Data.Contexts;
using InforceTestTask.Data.Repositories.Interfaces;
using InforceTestTask.Infrastructure.Services;
using InforceTestTask.Infrastructure.Services.Interfaces;
using InforceTestTask.Services.Interfaces;
using InforceTestTask.ViewModels;

namespace InforceTestTask.Services;

public class UrlsService : BaseDataService<UrlsDbContext>, IUrlsService
{
    private readonly IUrlsRepository _repository;
    private readonly ILogger<UrlsService> _logger;
    private readonly IMapper _mapper;

    public UrlsService(
        IUrlsRepository repository,
        IDbContextWrapper<UrlsDbContext> contextWrapper,
        ILogger<BaseDataService<UrlsDbContext>> baseLogger,
        ILogger<UrlsService> logger,
        IMapper mapper)
        : base(contextWrapper, baseLogger)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<int?> AddUrlAsync(string originalUrl, string createdBy)
    {
        var result = await ExecuteSafeAsync(() =>
        {
            var shortUrl = $"short.ua/{GenerateShortUrl(originalUrl)}";
            return _repository.AddUrlAsync(originalUrl, shortUrl, createdBy);
        });

        _logger.LogInformation($"Url with id {result} was created!");

        return result;
    }

    public async Task<bool> DeleteUrlAsync(int id)
    {
        var result = await ExecuteSafeAsync(() => _repository.DeleteUrlAsync(id));

        if (result)
        {
            _logger.LogInformation($"Url with id {id} has deleted!");
        }
        else
        {
            _logger.LogWarning($"Url with id {id} hasn't deleted!");
        }

        return result;
    }

    public Task<ShortUrlVM> GetUrlAsync(int id)
    {
        return ExecuteSafeAsync(async () =>
        {
            var urlEntity = await _repository.GetUrlAsync(id);
            var urlVM = _mapper.Map<ShortUrlVM>(urlEntity);

            if (urlVM is null)
            {
                _logger.LogWarning($"Url with id {id} hasn't found!");
            }
            else
            {
                _logger.LogInformation($"Url with id {id} has found and mapped to vm!");
            }

            return urlVM !;
        });
    }

    public Task<IEnumerable<ShortUrlVM>> GetUrlsListAsync()
    {
        return ExecuteSafeAsync(async () =>
        {
            var urlEntities = await _repository.GetUrlsListAsync();
            var urlVMs = urlEntities.Select(_mapper.Map<ShortUrlVM>);

            _logger.LogInformation($"{urlEntities.Count()} urls has found and mapped to vm!");

            return urlVMs;
        });
    }

    private static string GenerateShortUrl(string originalUrl)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(originalUrl));

        string hex = BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        string shortHex = hex.Substring(0, 15);

        byte[] shortBytes = Enumerable.Range(0, shortHex.Length - (shortHex.Length % 2))
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(shortHex.Substring(x, 2), 16))
                .ToArray();

        string base64 = Convert.ToBase64String(shortBytes);

        return base64;
    }
}
