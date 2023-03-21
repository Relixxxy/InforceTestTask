using InforceTestTask.Data.Contexts;
using InforceTestTask.Data.Entities;
using InforceTestTask.Data.Repositories.Interfaces;
using InforceTestTask.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace InforceTestTask.Data.Repositories;

public class UrlsRepository : IUrlsRepository
{
    private readonly UrlsDbContext _context;

    public UrlsRepository(UrlsDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddUrlAsync(string originalUrl, string shortUrl, string createdBy)
    {
        var url = new ShortUrlEntity
        {
            OriginalUrl = originalUrl,
            ShortUrl = shortUrl,
            CreatedBy = createdBy,
            CreatedDate = DateTime.Now,
        };

        if (_context.ShortUrls.Any(u => u.OriginalUrl == originalUrl))
        {
            throw new BusinessException("Url is already exists!", 409);
        }

        var result = await _context.AddAsync(url);
        await _context.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task<bool> DeleteUrlAsync(int id)
    {
        var url = await GetUrlAsync(id);

        if (url == null)
        {
            return false;
        }

        _context.Remove(url);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<ShortUrlEntity> GetUrlAsync(int id)
    {
        var url = await _context.ShortUrls.FirstOrDefaultAsync(x => x.Id == id);
        return url!;
    }

    public async Task<IEnumerable<ShortUrlEntity>> GetUrlsListAsync()
    {
        var list = await _context.ShortUrls.ToListAsync();
        return list;
    }

    public async Task<bool> UpdateUrlAsync(int id, string originalUrl, string shortUrl, string createdBy)
    {
        var url = await GetUrlAsync(id);

        if (url == null)
        {
            return false;
        }

        url.OriginalUrl = originalUrl;
        url.ShortUrl = shortUrl;
        url.CreatedBy = createdBy;

        await _context.SaveChangesAsync();

        return true;
    }
}
