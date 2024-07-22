using System.Net.Http.Json;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UrlShortener.Api;
using UrlShortener.Api.Aggregates.ShortenerAggregate;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Tests.Aggregates.ShortenerAggregate;

internal static class UrlShortenerHelper
{
    internal static async Task CreateWebApiAndExecute(IContainer sqlDatabase, Func<HttpClient, Task> function)
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
                    var services = collection.BuildServiceProvider();
                    var dbContext = services.GetRequiredService<UrlShortenerContext>();
                    dbContext.Database.EnsureCreated();
                });
            });
        using var httpClient = webApplication.CreateDefaultClient();
        await function(httpClient);
    }

    internal static async Task<(HttpResponseMessage?, CreateShortenedUrlResponse?)> CreateShortUrl(
        HttpClient httpClient,
        string longUrl
    )
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/shortener", new CreateShortenedUrlRequest(longUrl));
        var content = await response.Content.ReadFromJsonAsync<CreateShortenedUrlResponse>();
        return (response, content);
    }
}