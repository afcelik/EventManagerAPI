using EventManagerAPI.ORM.Context;
using EventManagerAPI.ORM.Dto.requestDto.Event;
using EventManagerAPI.ORM.Dto.responseDto.Event;
using EventManagerAPI.ORM.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        EventContext context = new EventContext();

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GetAllEventsResponseDto> data = context.Events.Select(x => new GetAllEventsResponseDto()
            {
                EventId = x.EventId,
                EventTitle = x.EventTitle,
                EventPerson = x.EventPerson,
                EventCategory = x.EventCategory,
                EventDescription = x.EventDescription,
                PlaceId = x.PlaceId,
                EventStartingDate = x.EventStartingDate,
                EventEndDate = x.EventEndDate,
                IsEventPaid = x.IsEventPaid,
                EventPrice = (decimal)x.EventPrice,
                EventImage1 = x.EventImage1,
                EventImage2 = x.EventImage2,
                EventImage3 = x.EventImage3,

            }).ToList();


            return Ok(data);

        }
        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {

            var activity = context.Events.FirstOrDefault(x => x.EventId == id);

            if (activity == null)
            {
                return NotFound(id + "Girilen idye sahip etkinlik bulunamadı.");
            }
            else
            {
                GetEventByIdResponseDto data = new GetEventByIdResponseDto();
                data.EventId = activity.EventId;
                data.EventTitle = activity.EventTitle;
                data.EventPerson = activity.EventPerson;
                data.EventCategory = activity.EventCategory;
                data.EventDescription = activity.EventDescription;
                data.PlaceId = activity.PlaceId;
                data.EventStartingDate = activity.EventStartingDate;
                data.EventEndDate = activity.EventEndDate;
                data.IsEventPaid = activity.IsEventPaid;
                data.EventPrice = (decimal)activity.EventPrice;
                data.EventImage1 = activity.EventImage1;
                data.EventImage2 = activity.EventImage2;
                data.EventImage3 = activity.EventImage3;
                
                return Ok(data);
            }

        }
        [HttpPost]
        public IActionResult Add(AddEventRequestDto request)
        {
            var activity = new Event
            {
                EventTitle = request.EventTitle,
                EventPerson = request.EventPerson,
                EventCategory = request.EventCategory,
                EventDescription = request.EventDescription,
                PlaceId = request.PlaceId,
                EventStartingDate = request.EventStartingDate,
                EventEndDate = request.EventEndDate,
                IsEventPaid = request.IsEventPaid,
                EventPrice = request.EventPrice,
                EventImage1 = request.EventImage1,
                EventImage2 = request.EventImage2,
                EventImage3 = request.EventImage3,
            };

            context.Events.Add(activity);
            context.SaveChanges();

            return Created("OK", request);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var activity = context.Events.FirstOrDefault(x => x.EventId == id);
            if (activity != null)
            {
                context.Events.Remove(activity);
                context.SaveChanges();
                return Ok("Silindi");
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEventRequestDto Event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Girilen id değerinde bir etkinlik mevcut değil!");
            }
            var existingActivity = context.Events.Where(p => p.EventId == id).FirstOrDefault<Event>();
            if (existingActivity != null)
            {
                existingActivity.EventTitle = Event.EventTitle;
                existingActivity.EventPerson = Event.EventPerson;
                existingActivity.EventCategory = Event.EventCategory;
                existingActivity.EventDescription = Event.EventDescription;
                existingActivity.PlaceId = Event.PlaceId;
                existingActivity.EventStartingDate = Event.EventStartingDate;
                existingActivity.EventEndDate = Event.EventEndDate;
                existingActivity.IsEventPaid = Event.IsEventPaid;
                existingActivity.EventPrice = Event.EventPrice;
                existingActivity.EventImage1 = Event.EventImage1;
                existingActivity.EventImage2 = Event.EventImage2;
                existingActivity.EventImage3 = Event.EventImage3;


                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            List<GetAllEventsResponseDto> response = context.Events.Select(x => new GetAllEventsResponseDto
            {
                EventId = x.EventId,
                EventTitle = x.EventTitle,
                EventPerson = x.EventPerson,
                EventCategory = x.EventCategory,
                EventDescription = x.EventDescription,
                PlaceId = x.PlaceId,
                EventStartingDate = x.EventStartingDate,
                EventEndDate = x.EventEndDate,
                IsEventPaid = x.IsEventPaid,
                EventPrice = (decimal)x.EventPrice,
                EventImage1 = x.EventImage1,
                EventImage2 = x.EventImage2,
                EventImage3 = x.EventImage3,

            }).ToList();
            return Ok(response);
        }
    }
}
