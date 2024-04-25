using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace TestContainers;

[CollectionDefinition(nameof(SqlDatabaseCollection))]
public class SqlDatabaseCollection : ICollectionFixture<SqlDatabaseCollection>;

public class SqlDatabaseFixture : IAsyncLifetime
{
    public readonly MsSqlContainer  MsSqlContainer =
        new MsSqlBuilder()
            .WithImage("dangl/mssql-tmpfs:latest")
            .WithTmpfsMount("/var/opt/mssql/data")
            .WithTmpfsMount("/var/opt/mssql/log")
            .WithTmpfsMount("/var/opt/mssql/secrets")
            .Build();
    
    public async Task InitializeAsync() 
        => await MsSqlContainer.StartAsync();

    public async Task DisposeAsync() 
        => await MsSqlContainer.DisposeAsync();
}


public class UnitTest2(SqlDatabaseFixture fixture) : IClassFixture<SqlDatabaseFixture>
{
    [Fact]
    public async Task ReadFromMsSqlDatabase()
    {
        
        
        await using var connection = new SqlConnection(fixture.MsSqlContainer.GetConnectionString());
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT 1;";

        var actual = await command.ExecuteScalarAsync() as int?;
        Assert.Equal(1, actual.GetValueOrDefault());
    }
}