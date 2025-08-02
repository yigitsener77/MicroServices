using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RESTful_Mongo.Data
{
    public class Recipe : Base
    {
        public string Name { get; set; } = null!;
        public IEnumerable<string> Ingredients { get; set; } = [];
        public IEnumerable<string> Instructions { get; set; } = [];

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!; // Reference to the User who created the recipe
    }
}
