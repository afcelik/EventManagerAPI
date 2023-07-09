using EventManagerAPI.ORM.Context;
using EventManagerAPI.ORM.Dto.requestDto.Event;
using EventManagerAPI.ORM.Dto.responseDto.Event;
using EventManagerAPI.ORM.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        EventContext context = new EventContext();

        [HttpGet]
        public IActionResult GetAllEventWithPlace()
        {
            var data = context.Events
                .Join(context.Places,
                    e => e.PlaceId,
                    p => p.PlaceId,
                    (e, p) => new GetAllEventsWithPlaceResponseDto
                    {
                        EventId = e.EventId,
                        EventTitle = e.EventTitle,
                        EventPerson = e.EventPerson,
                        EventCategory = e.EventCategory,
                        EventCity = e.EventCity,
                        EventDescription = e.EventDescription,
                        PlaceId = e.PlaceId,
                        EventStartingDate = e.EventStartingDate,
                        EventEndDate = e.EventEndDate,
                        IsEventPaid = e.IsEventPaid,
                        EventPrice = (decimal)e.EventPrice,
                        EventImageUrlOne = e.EventImageUrlOne,
                        EventImageUrlTwo = e.EventImageUrlTwo,
                        EventImageUrlThree = e.EventImageUrlThree,
                        PlaceName = p.PlaceName
                    })
                .ToList();

            return Ok(data);
        }


        [HttpGet("popular-events")]
        public IActionResult GetPopularEvents()
        {
            var popularEvents = context.Events
                .Select(e => new
                {
                    Event = e,
                    TicketCount = e.Tickets.Count
                })
                .OrderByDescending(e => e.TicketCount)
                .Take(3)
                .Select(e => new GetPopularEventsResponseDto
                {
                    EventId = e.Event.EventId,
                    EventTitle = e.Event.EventTitle,
                    EventPerson = e.Event.EventPerson,
                    EventCategory = e.Event.EventCategory,
                    EventCity = e.Event.EventCity,
                    EventDescription = e.Event.EventDescription,
                    PlaceId = e.Event.PlaceId,
                    EventStartingDate = e.Event.EventStartingDate,
                    EventEndDate = e.Event.EventEndDate,
                    IsEventPaid = e.Event.IsEventPaid,
                    EventPrice = (decimal)e.Event.EventPrice,
                    EventImageUrlOne = e.Event.EventImageUrlOne,
                    EventImageUrlTwo = e.Event.EventImageUrlTwo,
                    EventImageUrlThree = e.Event.EventImageUrlThree,
                    TicketCount = e.TicketCount
                })
                .ToList();

            return Ok(popularEvents);
        }
        [HttpGet("categories-list")]
        public IActionResult GetCategoriesList()
        {
            var events = context.Events
                .Select(x => new GetAllEventCategoriesResponseDto
                {
                    EventCategory = x.EventCategory
                })
                .ToList();

            var groupedData = events
                .GroupBy(x => x.EventCategory)
                .Select(x => x.First())
                .ToList();

            return Ok(groupedData);
        }

        [HttpGet("cities-list")]
        public IActionResult GetCitiesList()
        {
            var events = context.Events
                .Select(x => new GetAllEventCitiesResponseDto
                {
                    EventCity = x.EventCity
                })
                .ToList();

            var groupedData = events
                .GroupBy(x => x.EventCity)
                .Select(x => x.First())
                .ToList();

            return Ok(groupedData);
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
                data.EventCity = activity.EventCity;
                data.EventDescription = activity.EventDescription;
                data.PlaceId = activity.PlaceId;
                data.EventStartingDate = activity.EventStartingDate;
                data.EventEndDate = activity.EventEndDate;
                data.IsEventPaid = activity.IsEventPaid;
                data.EventPrice = (decimal)activity.EventPrice;
                data.EventImageUrlOne = activity.EventImageUrlOne;
                data.EventImageUrlTwo = activity.EventImageUrlTwo;
                data.EventImageUrlThree = activity.EventImageUrlThree;
                
                return Ok(data);
            }

        }

        [HttpGet("event-address/{id}")]
        public IActionResult GetEventAddressById(int id)
        {
            var eventWithPlace = context.Events
                                .Join(context.Places, e => e.PlaceId, p => p.PlaceId, (e, p) => new { Event = e, Place = p })
                                .FirstOrDefault(x => x.Event.EventId == id);

            if (eventWithPlace == null)
            {
                return NotFound(id + "Girilen idye sahip etkinlik bulunamadı.");
            }
            else
            {
                string address = eventWithPlace.Place.PlaceAddress;
                return Ok(address);
            }
        }

        [HttpGet("place-name/{id}")]
        public IActionResult GetPlaceNameById(int id)
        {
            var eventWithPlace = context.Events
                                .Join(context.Places, e => e.PlaceId, p => p.PlaceId, (e, p) => new { Event = e, Place = p })
                                .FirstOrDefault(x => x.Event.EventId == id);

            if (eventWithPlace == null)
            {
                return NotFound(id + "Girilen idye sahip etkinlik bulunamadı.");
            }
            else
            {
                string place = eventWithPlace.Place.PlaceName;
                return Ok(place);
            }
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventRequestDto request)
        {
            var place = context.Places.FirstOrDefault(p => p.PlaceId == request.PlaceId);

            if (place == null)
            {
                return BadRequest("Invalid PlaceId");
            }

            var activity = new Event
            {
                EventTitle = request.EventTitle,
                EventPerson = request.EventPerson,
                EventCategory = request.EventCategory,
                EventCity = request.EventCity,
                EventDescription = request.EventDescription,
                PlaceId = request.PlaceId,
                EventStartingDate = request.EventStartingDate,
                EventEndDate = request.EventEndDate,
                IsEventPaid = request.IsEventPaid,
                EventPrice = request.EventPrice,
                EventImageUrlOne = request.EventImageUrlOne,
                EventImageUrlTwo = request.EventImageUrlTwo,
                EventImageUrlThree = request.EventImageUrlThree,
                PlaceName = place.PlaceName
            };

            context.Events.Add(activity);
            context.SaveChanges();

            return Created("OK", request);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
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
        public IActionResult UpdateEvent(int id, [FromBody] UpdateEventRequestDto Event)
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
                existingActivity.EventCity = Event.EventCity;
                existingActivity.EventDescription = Event.EventDescription;
                existingActivity.PlaceId = Event.PlaceId;
                existingActivity.EventStartingDate = Event.EventStartingDate;
                existingActivity.EventEndDate = Event.EventEndDate;
                existingActivity.IsEventPaid = Event.IsEventPaid;
                existingActivity.EventPrice = Event.EventPrice;
                existingActivity.EventImageUrlOne = Event.EventImageUrlOne;
                existingActivity.EventImageUrlTwo = Event.EventImageUrlTwo;
                existingActivity.EventImageUrlThree = Event.EventImageUrlThree;


                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            List<GetAllEventsWithPlaceResponseDto> response = context.Events.Select(x => new GetAllEventsWithPlaceResponseDto
            {
                EventId = x.EventId,
                EventTitle = x.EventTitle,
                EventPerson = x.EventPerson,
                EventCategory = x.EventCategory,
                EventCity = x.EventCity,
                EventDescription = x.EventDescription,
                PlaceId = x.PlaceId,
                EventStartingDate = x.EventStartingDate,
                EventEndDate = x.EventEndDate,
                IsEventPaid = x.IsEventPaid,
                EventPrice = (decimal)x.EventPrice,
                EventImageUrlOne = x.EventImageUrlOne,
                EventImageUrlTwo = x.EventImageUrlTwo,
                EventImageUrlThree = x.EventImageUrlThree,

            }).ToList();
            return Ok(response);
        }
    }
}
