using InforceTestTask.Data.Entities;
using InforceTestTask.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace InforceTestTask.Data.Contexts;

public class UrlsDbContext : DbContext
{
    public UrlsDbContext(DbContextOptions<UrlsDbContext> options)
        : base(options)
    {
    }

    public DbSet<ShortUrlEntity> ShortUrls { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ShortUrlEntityConfiguration());
    }
}
