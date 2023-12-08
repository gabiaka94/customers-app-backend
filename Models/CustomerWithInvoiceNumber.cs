namespace CustomerStoreApi.Models;

public class CustomerWithInvoiceNumber : Customer {
    public long? InvoicesCount { get; set; }
}