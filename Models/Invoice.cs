using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerStoreApi.Models;

public class Invoice
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? CustomerId { get; set; }

    public long Number { get; set; }

    public DateTime Date { get; set; }
    public long Total { get; set; }

}