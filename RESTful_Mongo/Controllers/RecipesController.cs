using Microsoft.AspNetCore.Mvc;
using RESTful_Mongo.Data;
using RESTful_Mongo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTful_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController(RecipeService recipeService, UserService userService) : ControllerBase
    {
        private readonly RecipeService recipeService = recipeService;
        private readonly UserService userService = userService;

        // GET: api/<RecipesController>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await recipeService.GetAllAsync());

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var recipe = await recipeService.GetAsync(id.ToString());
            return recipe == null ? NotFound() : Ok(recipe);
        }

        // POST api/<RecipesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Recipe recipe)
        {
            if (recipe == null) return BadRequest("Recipe cannot be null");

            await recipeService.CreateAsync(recipe);
            await userService.AddRecipeAsync(recipe.UserId, recipe.Id);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Recipe newRecipe)
        {
            if (newRecipe == null || newRecipe.Id != id) return BadRequest("Recipe cannot be null and ID must match");

            if (await recipeService.GetAsync(id) == null) return NotFound();

            await recipeService.UpdateAsync(id, newRecipe);
            return NoContent();
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var recipe = await recipeService.GetAsync(id);
            if (recipe == null) return NotFound();

            await recipeService.DeleteAsync(id);
            await userService.RemoveRecipeAsync(recipe.UserId, id);
            return NoContent();
        }
    }
}
