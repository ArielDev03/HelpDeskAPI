using HelpDeskAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace HelpDeskAPI.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTickets();

            return Ok(tickets);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int id)
        {
            var ticket = await _ticketService.GetTicketById(id);

            return Ok(ticket);

        }
    }
}
