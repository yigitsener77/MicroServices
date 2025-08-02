using Microsoft.Extensions.Options;
using RESTful_Mongo.Data;

namespace RESTful_Mongo.Services
{
    public class RecipeService(IOptions<DatabaseSettings> options) : BaseService<Recipe>(options, "Recipes")
    {

    }
}
