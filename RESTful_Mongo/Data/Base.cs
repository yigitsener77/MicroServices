using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RESTful_Mongo.Data
{
    public abstract class Base
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!; // Using null-forgiving operator to indicate that this will be set later
    }
}
