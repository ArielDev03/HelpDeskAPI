using HelpDeskAPI.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/ticket-statuses")]
    public class TicketStatusController : ControllerBase
    {
        private readonly ITicketStatusService _statusService;

        public TicketStatusController(ITicketStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _statusService.GetAllStatuses();

            return Ok(statuses);
        }
    }
}
