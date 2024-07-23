using System.Net.Http.Json;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UrlShortener.Api;
using UrlShortener.Api.Aggregates.ShortenerAggregate;
using UrlShortener.Infrastructure.Aggregates.UrlAggregate;

namespace UrlShortener.Tests.Aggregates.ShortenerAggregate;

internal static class ShortenerHelper
{
    internal static async Task CreateWebApiAndExecute(
        IContainer sqlDatabase,
        Func<IServiceProvider, HttpClient, Task> function
    )
    {
        await using var webApplication = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(x =>
            {
                x.UseEnvironment(Environments.Development);
                x.UseSetting(
                    "DB_CONNECTION_STRING",
                    $"Server=localhost;Port={sqlDatabase.GetMappedPublicPort(5432)};"
                    + $"Database=postgres;User Id=postgres;Password=postgres;"
                );
                x.ConfigureServices(collection =>
                {
                    using var serviceProvider = collection.BuildServiceProvider();
                    using var dbContext = serviceProvider.GetRequiredService<UrlContext>();
                    dbContext.Database.EnsureCreated();
                });
            });

        using var httpClient = webApplication.CreateDefaultClient();
        await function(webApplication.Services, httpClient);
    }

    internal static Task<HttpResponseMessage> CreateShortUrl(HttpClient httpClient, string longUrl)
    {
        return httpClient.PostAsJsonAsync("/api/v1/shortener", new CreateShortenedUrlRequest(longUrl));
    }

    internal static Task<CreateShortenedUrlResponse?> ReadFromShortUrlResponse(HttpResponseMessage response)
    {
        return response.Content.ReadFromJsonAsync<CreateShortenedUrlResponse>();
    }

    internal static Task<HttpResponseMessage> GetRedirectResponse(HttpClient httpClient, string? shortUrl)
    {
        return httpClient.GetAsync($"api/v1/shortener/{shortUrl}");
    }
}