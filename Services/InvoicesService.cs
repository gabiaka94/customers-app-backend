using CustomerStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CustomerStoreApi.Services;

public class InvoicesService
{
    private readonly IMongoCollection<Invoice>  _invoicesCollection;

    public InvoicesService(
        IOptions<CustomerStoreDatabaseSettings> CustomerStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            CustomerStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            CustomerStoreDatabaseSettings.Value.DatabaseName);

         _invoicesCollection = mongoDatabase.GetCollection<Invoice>(
            CustomerStoreDatabaseSettings.Value.InvoiceCollectionName);
    }

    public async Task<List<Invoice>> GetAsync() =>
        await  _invoicesCollection.Find(_ => true).ToListAsync();

public async Task<List<Invoice>> GetByCustomerId(string customerId) =>
        await  _invoicesCollection.Find(x => x.CustomerId == customerId).ToListAsync();
        
    public async Task<long?> GetTotalByCustomer(string customerId) =>
        await  _invoicesCollection.Find(x => x.CustomerId == customerId).CountDocumentsAsync();

}