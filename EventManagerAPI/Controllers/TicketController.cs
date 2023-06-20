using EventManagerAPI.ORM.Context;
using EventManagerAPI.ORM.Dto.requestDto.Ticket;
using EventManagerAPI.ORM.Dto.responseDto.Ticket;
using EventManagerAPI.ORM.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        EventContext context = new EventContext();

        [HttpGet]
        public IActionResult GetAllTickets()
        {
            List<GetAllTicketsResponseDto> data = context.Tickets.Select(x => new GetAllTicketsResponseDto()
            {
                TicketId = x.TicketId,
                CustomerId = x.CustomerId,
                EventId = x.EventId,
                TicketType = x.TicketType,
                TicketPurchaseDate = x.TicketPurchaseDate,
                TicketPrice = x.TicketPrice,

            }).ToList();


            return Ok(data);

        }
        [HttpGet("{id}")]
        public IActionResult GetTicket(int id)
        {

            var ticket = context.Tickets.FirstOrDefault(x => x.TicketId == id);

            if (ticket == null)
            {
                return NotFound(id + "Girilen idye sahip bilet bulunamadı.");
            }
            else
            {
                GetTicketByIdResponseDto data = new GetTicketByIdResponseDto();
                data.TicketId = ticket.TicketId;
                data.CustomerId = ticket.CustomerId;
                data.EventId = ticket.EventId;
                data.TicketType = ticket.TicketType;
                data.TicketPurchaseDate = ticket.TicketPurchaseDate;
                data.TicketPrice = ticket.TicketPrice;

                return Ok(data);
            }

        }
        [HttpPost]
        public IActionResult AddTicket(AddTicketRequestDto request)
        {
            var ticket = new Ticket
            {
                CustomerId = request.CustomerId,
                EventId = request.EventId,
                TicketType = request.TicketType,
                TicketPurchaseDate = request.TicketPurchaseDate,
                TicketPrice = request.TicketPrice,
            };

            context.Tickets.Add(ticket);
            context.SaveChanges();

            return Created("OK", request);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(x => x.TicketId == id);
            if (ticket != null)
            {
                context.Tickets.Remove(ticket);
                context.SaveChanges();
                return Ok("Silindi");
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, [FromBody] UpdateTicketRequestDto Ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Girilen id değerinde bir bilet mevcut değil!");
            }
            var existingTicket = context.Tickets.Where(p => p.TicketId == id).FirstOrDefault<Ticket>();
            if (existingTicket != null)
            {
                existingTicket.CustomerId = Ticket.CustomerId;
                existingTicket.EventId = Ticket.EventId;
                existingTicket.TicketType = Ticket.TicketType;
                existingTicket.TicketPurchaseDate = Ticket.TicketPurchaseDate;
                existingTicket.TicketPrice = Ticket.TicketPrice;

                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            List<GetAllTicketsResponseDto> response = context.Tickets.Select(x => new GetAllTicketsResponseDto
            {
                TicketId = x.TicketId,
                CustomerId = x.CustomerId,
                EventId = x.EventId,
                TicketType = x.TicketType,
                TicketPurchaseDate = x.TicketPurchaseDate,
                TicketPrice = x.TicketPrice,

            }).ToList();
            return Ok(response);
        }
    }
}
