using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Aggregates.UrlAggregate;

namespace UrlShortener.Infrastructure.Contexts;

public class UrlShortenerContext : DbContext
{
    public DbSet<UrlEntity> Urls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UrlEntity>(builder =>
        {
            builder.ToTable(nameof(Urls));
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.LongUrl, x.ShortUrl }).IsUnique();
            builder.HasIndex(x => x.ShortUrl).IsUnique();
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(x => x.LongUrl).IsRequired();
            builder.Property(x => x.ShortUrl).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.ExpiresAt).IsRequired(false);
            builder.Property(x => x.DeletedAt).IsRequired(false);
        });
    }
}