using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

public class Order
{
    [BsonId]
    [BsonElement("_idOrder"), BsonRepresentation(BsonType.ObjectId)]
    public string? IdOrder { get; set; }

    [BsonElement("date")]
    public DateTime? Date { get; set; }

    [BsonElement("status")]
    public string? Status { get; set; }

    [BsonElement("product")]
    public List<string>? Products { get; set; }

    [BsonElement("clientId")]
    public string? ClientId { get; set; }

    public Order()
    {
        Products = new List<string>();
    }
}
