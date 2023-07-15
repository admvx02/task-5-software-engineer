using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public TodosController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos");

            if (response.IsSuccessStatusCode)
            {
                var todos = await response.Content.ReadFromJsonAsync<object>();
                return Ok(todos);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve todos");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var todo = await response.Content.ReadFromJsonAsync<object>();
                return Ok(todo);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve todo");
            }
        }
    }
}
