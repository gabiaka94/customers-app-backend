using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerStoreApi.Models;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = null!;

    public string Name { get; set; }  = null!;

    public string Address { get; set; }  = null!;
    public string State { get; set; }  = null!;

    public string Country { get; set; }  = null!;

    public string? Subscription { get; set; }  = null!;
}