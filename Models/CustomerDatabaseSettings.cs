namespace CustomerStoreApi.Models;

public class CustomerStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CustomerCollectionName { get; set; } = null!;
     public string InvoiceCollectionName { get; set; } = null!;
}