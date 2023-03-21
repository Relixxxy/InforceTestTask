using InforceTestTask.Data.Entities;

namespace InforceTestTask.Data.Repositories.Interfaces;

public interface IUrlsRepository
{
    Task<ShortUrlEntity> GetUrlAsync(int id);
    Task<IEnumerable<ShortUrlEntity>> GetUrlsListAsync();
    Task<int?> AddUrlAsync(string originalUrl, string shortUrl, string createdBy);
    Task<bool> UpdateUrlAsync(int id, string originalUrl, string shortUrl, string createdBy);
    Task<bool> DeleteUrAsync(int id);
}
