namespace Parmenion.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/contributions")]
    [ApiController]
    public class ContributionsController : Controller
    {
        [HttpPost]
        public ActionResult SaveForLater()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult FindContributions()
        {
            return NotFound();
        }
    }
}
