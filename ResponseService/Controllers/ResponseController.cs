using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        public ResponseController()
        {

        }

        // Get /api/response/100
        [Route("{id:int}")]
        [HttpGet]
        public ActionResult GetRespose(int id)
        {
            Random random = new Random();
            var myRandom = random.Next(1, 101);

            if (myRandom >= id)
            {
                Console.WriteLine("--> Failure - Generate a HTTP 500");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Console.WriteLine("--> Success - Generate a HTTP 500");
            return Ok();
        }
    }
}