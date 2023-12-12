using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly AgentsContext _context;
        public BuggyController(AgentsContext context)
        {
            _context = context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRquest()
        {
            var thing = _context.Agents.Find(42);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetNotServerError()
        {
            var thing = _context.Agents.Find(42);

            var thingToReturn = thing.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRquest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRquest(int id)
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("testauth")]

        public ActionResult<string> GetSecretText()
        {
            return "secret Stuff";
        }

    }
}