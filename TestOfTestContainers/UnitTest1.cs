using MongoDB.Bson;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace TestContainers;

[CollectionDefinition(nameof(MongoDatabaseCollection))]
public class MongoDatabaseCollection : ICollectionFixture<MongoDatabaseFixture>;

public class MongoDatabaseFixture : IAsyncLifetime
{
    public readonly MongoDbContainer MongoDbContainer =
        new MongoDbBuilder()
            .WithTmpfsMount("/data/db")
            .Build();
    
    public async Task InitializeAsync() 
        => await MongoDbContainer.StartAsync();

    public async Task DisposeAsync() 
        => await MongoDbContainer.DisposeAsync();
}

public class UnitTest1(MongoDatabaseFixture fixture) : IClassFixture<MongoDatabaseFixture>
{
    [Fact]
    public async Task ReadFromMongoDbDatabase1()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        var mongoDatabase = client.GetDatabase(Guid.NewGuid().ToString());
        mongoDatabase.CreateCollection("test");
        var collection = mongoDatabase.GetCollection<BsonDocument>("test");
        await collection.InsertOneAsync(new BsonDocument("name", "value"));

        var list = mongoDatabase.GetCollection<BsonDocument>("test").Find(new BsonDocument()).ToList();

        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase2()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        var mongoDatabase = client.GetDatabase(Guid.NewGuid().ToString());
        mongoDatabase.CreateCollection("test");
        var collection = mongoDatabase.GetCollection<BsonDocument>("test");
        await collection.InsertOneAsync(new BsonDocument("name", "value2"));

        var list = mongoDatabase.GetCollection<BsonDocument>("test").Find(new BsonDocument()).ToList();
        
        
        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase3()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase4()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase5()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase6()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Fact]
    public async Task ReadFromMongoDbDatabase7()
    {
        var client = new MongoClient(fixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
}