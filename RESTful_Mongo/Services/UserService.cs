using Microsoft.Extensions.Options;
using RESTful_Mongo.Data;

namespace RESTful_Mongo.Services
{

    public class UserService(IOptions<DatabaseSettings> options) : BaseService<User>(options, "Users")
    {
        public async Task AddRecipeAsync(string userId, string recipeId)
        {
            var user = await GetAsync(userId) ?? throw new ArgumentException($"User with ID {userId} does not exist.");
            user.RecipeIds.Append(recipeId);
            await UpdateAsync(userId, user);
        }

        public async Task RemoveRecipeAsync(string userId, string recipeId)
        {
            var user = await GetAsync(userId) ?? throw new ArgumentException($"User with ID {userId} does not exist.");
            if (user.RecipeIds.Contains(recipeId))
            {
                user.RecipeIds = user.RecipeIds.Where(id => id != recipeId).ToArray();
                await UpdateAsync(userId, user);
            }
            else
            {
                throw new ArgumentException($"Recipe with ID {recipeId} is not associated with User {userId}.");
            }
        }
    }
}
