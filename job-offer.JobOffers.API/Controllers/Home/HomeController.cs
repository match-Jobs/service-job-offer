using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace job_offer.JobOffers.API.Controllers.Home
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status200OK, "Home!!!");
        }
    }
}
