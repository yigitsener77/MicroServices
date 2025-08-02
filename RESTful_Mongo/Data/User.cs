using MongoDB.Bson.Serialization.Attributes;

namespace RESTful_Mongo.Data
{

    public class User : Base
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        [BsonElement("recipeIds")]
        public IEnumerable<string> RecipeIds { get; set; } = []; // List of recipe IDs created by the user
    }
}
