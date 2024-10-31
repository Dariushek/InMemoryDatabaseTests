using MongoDB.Bson;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace TestOfTestContainersNunit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class Tests
{
    [Test]
    public async Task ReadFromMongoDbDatabase1()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        var mongoDatabase = client.GetDatabase(Guid.NewGuid().ToString());
        mongoDatabase.CreateCollection("test");
        var collection = mongoDatabase.GetCollection<BsonDocument>("test");
        await collection.InsertOneAsync(new BsonDocument("name", "value"));

        var list = mongoDatabase.GetCollection<BsonDocument>("test").Find(new BsonDocument()).ToList();

        Assert.True(await databases.AnyAsync());
    }
    
    [Test]
    public async Task ReadFromMongoDbDatabase2()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        var mongoDatabase = client.GetDatabase(Guid.NewGuid().ToString());
        mongoDatabase.CreateCollection("test");
        var collection = mongoDatabase.GetCollection<BsonDocument>("test");
        await collection.InsertOneAsync(new BsonDocument("name", "value2"));

        var list = mongoDatabase.GetCollection<BsonDocument>("test").Find(new BsonDocument()).ToList();
        
        
        Assert.True(await databases.AnyAsync());
    }
    
    [Test]
    public async Task ReadFromMongoDbDatabase3()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Test]
    public async Task ReadFromMongoDbDatabase4()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Test]
    public async Task ReadFromMongoDbDatabase5()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
    
    [Test]
    public async Task ReadFromMongoDbDatabase6()
    {
        var client = new MongoClient(GlobalSetup.MongoDatabaseFixture.MongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }
}

public class MongoDatabaseFixture
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

[SetUpFixture]
public class GlobalSetup
{
    public static MongoDatabaseFixture MongoDatabaseFixture;

    [OneTimeSetUp]
    public async Task GlobalOneTimeSetUp()
    {
        // Initialize the shared MongoDB fixture
        MongoDatabaseFixture = new MongoDatabaseFixture();
        await MongoDatabaseFixture.InitializeAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalOneTimeTearDown()
    {
        // Dispose the MongoDB fixture
        if (MongoDatabaseFixture != null)
        {
            await MongoDatabaseFixture.DisposeAsync();
        }
    }
}