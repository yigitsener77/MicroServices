using Microsoft.AspNetCore.Mvc;
using RESTful_Mongo.Data;
using RESTful_Mongo.Services;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTful_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : ControllerBase
    {
        private readonly UserService userService = userService;

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await userService.GetAllAsync());

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await userService.GetAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user == null) return BadRequest("User cannot be null");

            await userService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] User newUser)
        {
            if (newUser == null || newUser.Id != id) return BadRequest("User cannot be null and ID must match");

            if (await userService.GetAsync(id) == null) return NotFound();

            await userService.UpdateAsync(id, newUser);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await userService.GetAsync(id) == null) return NotFound();

            await userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
