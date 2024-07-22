using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Testcontainers.PostgreSql;

namespace UrlShortener.Tests.Aggregates;

public class CoreFixture : IAsyncLifetime
{
    public INetwork Network { get; }
    public PostgreSqlContainer SqlDatabase { get; }

    public CoreFixture()
    {
        Network = new NetworkBuilder()
            .WithName(Guid.NewGuid().ToString())
            .Build();

        SqlDatabase = new PostgreSqlBuilder()
            .WithImage("postgres:alpine")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithDatabase("postgres")
            .WithNetwork(Network)
            .WithPortBinding(5432, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();
    }

    public async Task InitializeAsync()
    {
        await Network.CreateAsync();
        await SqlDatabase.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Network.DisposeAsync();
        await SqlDatabase.StopAsync();
    }
}