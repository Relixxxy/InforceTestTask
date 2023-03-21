using InforceTestTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InforceTestTask.Data.EntityConfigurations;

public class ShortUrlEntityConfiguration : IEntityTypeConfiguration<ShortUrlEntity>
{
    public void Configure(EntityTypeBuilder<ShortUrlEntity> builder)
    {
        builder.ToTable("ShortUrls").HasKey(sh => sh.Id);
        builder.Property(sh => sh.Id).HasColumnName("ShortUrlId").ValueGeneratedOnAdd();
        builder.Property(sh => sh.CreatedBy).HasMaxLength(100).IsRequired();
        builder.Property(sh => sh.OriginalUrl).HasMaxLength(2000).IsRequired();
        builder.Property(sh => sh.ShortUrl).HasMaxLength(30).IsRequired();
        builder.Property(sh => sh.CreatedDate).IsRequired();
    }
}
