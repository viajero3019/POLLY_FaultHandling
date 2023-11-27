using Microsoft.AspNetCore.Mvc;
using RequestService.Policies;

namespace RequestService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ClientPolicy _clientPolicy;
        private readonly IHttpClientFactory _clientFactory;

        public RequestController(ClientPolicy clientPolicy, IHttpClientFactory clientFactory)
        {
            _clientPolicy = clientPolicy;
            _clientFactory = clientFactory;
        }

        // GET api/request
        [HttpGet]
        public async Task<ActionResult> MakeRequest()
        {
            // var client = new HttpClient();

            var clientFactory = _clientFactory.CreateClient();

            // var clientFromDI = _clientFactory.CreateClient("MyPolicy");

            // var response = await client.GetAsync("https://localhost:7251/api/response/25");

            var response = await _clientPolicy.ImmediateHttpRetry.ExecuteAsync(
                () => clientFactory.GetAsync("https://localhost:7251/api/response/25")
            );

            // var response = await _clientPolicy.LinearHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7251/api/response/25")
            // );

            // var response = await _clientPolicy.ExponentialHttpRetry.ExecuteAsync(
            //     () => client.GetAsync("https://localhost:7251/api/response/25")
            // );

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Response returned SUCCESS");
                return Ok();
            }
            Console.WriteLine("--> Response returned FAILURE");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}