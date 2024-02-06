using Microsoft.AspNetCore.Mvc;

namespace CrazyOrders.API.Controllers
{
    [ApiController]
    [Route("health-check")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Status = "ok" });
        }
    }
}
