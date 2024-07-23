using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortener.Api.Aggregates.ShortenerAggregate;
using UrlShortener.Domain.Aggregates.UrlAggregate;
using UrlShortener.Infrastructure.Contexts;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var dbConnection = builder.Configuration.GetValue<string>("DB_CONNECTION_STRING");
        builder.Services.AddNpgsql<UrlContext>(dbConnection);

        builder.Services.Configure<ShortenerOptions>(options =>
        {
            options.ShortenerDomain = builder.Configuration.GetValue<string>("SHORTENER_DOMAIN")!;
        });

        builder.Services.TryAddScoped<IUrlRepository, UrlRepository>();
        builder.Services.TryAddScoped<IShortenerService, ShortenerService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}