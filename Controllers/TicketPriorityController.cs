using HelpDeskAPI.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/ticket-priorities")]
    public class TicketPriorityController : ControllerBase
    {
        private readonly ITicketPriorityService _priorityService;

        public TicketPriorityController(
            ITicketPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriorities()
        {
            var priorities = await _priorityService.GetAllPriorities();

            return Ok(priorities);
        }
    }
}
