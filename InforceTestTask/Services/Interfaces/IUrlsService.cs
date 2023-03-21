using InforceTestTask.ViewModels;

namespace InforceTestTask.Services.Interfaces;

public interface IUrlsService
{
    Task<ShortUrlVM> GetUrlAsync(int id);
    Task<IEnumerable<ShortUrlVM>> GetUrlsListAsync();
    Task<int?> AddUrlAsync(string originalUrl, string createdBy);
    Task<bool> DeleteUrAsync(int id);
}
