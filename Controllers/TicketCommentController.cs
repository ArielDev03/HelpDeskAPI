using HelpDeskAPI.DTOs.TicketsComments;
using HelpDeskAPI.Interfaces.Services;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Services.Tickets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskAPI.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketCommentController : ControllerBase
    {
        private readonly ITicketCommentService _ticketCommentService;
        public TicketCommentController (ITicketCommentService ticketCommentService)
        {
            _ticketCommentService = ticketCommentService;
        }

        [HttpGet("{ticketId}/comments")]
        public async Task<IActionResult> GetAllTicketsComments(int ticketId)
        {
            var ticketsComments = await _ticketCommentService.GetAllTicketsComments(ticketId);

            return Ok(ticketsComments);
        }

        [HttpPost("{ticketId}/comments")]
        public async Task<IActionResult> CreateTicketsComments(CreateTicketCommentDto commentDto, int ticketId)
        {
            var comments = await _ticketCommentService.CreateTicketsComments(commentDto, ticketId);


            return StatusCode(201, commentDto);
            
        }
    }
}
