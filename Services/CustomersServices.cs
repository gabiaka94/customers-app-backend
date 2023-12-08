using CustomerStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CustomerStoreApi.Services;

public class CustomersService
{
    private readonly IMongoCollection<Customer> _customersCollection;

    public CustomersService(
        IOptions<CustomerStoreDatabaseSettings> CustomerStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            CustomerStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CustomerStoreDatabaseSettings.Value.DatabaseName);

        _customersCollection = mongoDatabase.GetCollection<Customer>(
           CustomerStoreDatabaseSettings.Value.CustomerCollectionName);
    }

    public async Task<List<Customer>> GetAsync() =>
        await _customersCollection.Find(_ => true).ToListAsync();

    public async Task<Customer?> GetAsync(string id) =>
        await _customersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Customer newCustomer) =>
        await _customersCollection.InsertOneAsync(newCustomer);

    public async Task UpdateAsync(string id, Customer updatedCustomer) =>
        await _customersCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

    public async Task RemoveAsync(string id) =>
        await _customersCollection.DeleteOneAsync(x => x.Id == id);
}