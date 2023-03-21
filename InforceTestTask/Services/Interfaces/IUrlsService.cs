using InforceTestTask.ViewModels;

namespace InforceTestTask.Services.Interfaces;

public interface IUrlsService
{
    Task<ShortUrlVM> GetShortUrl(int id);
}
