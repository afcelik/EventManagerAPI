using EventManagerAPI.ORM.Context;
using EventManagerAPI.ORM.Dto.requestDto.Place;
using EventManagerAPI.ORM.Dto.responseDto.Place;
using EventManagerAPI.ORM.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : Controller
    {
        EventContext context = new EventContext();

        [HttpGet]
        public IActionResult GetAllPlaces()
        {
            List<GetAllPlacesResponseDto> data = context.Places.Select(x => new GetAllPlacesResponseDto()
            {
                PlaceId = x.PlaceId,
                PlaceName = x.PlaceName,
                PlaceAddress = x.PlaceAddress,

            }).ToList();


            return Ok(data);

        }
        [HttpGet("{id}")]
        public IActionResult GetPlace(int id)
        {

            var place = context.Places.FirstOrDefault(x => x.PlaceId == id);

            if (place == null)
            {
                return NotFound(id + "Girilen idye sahip mekan bulunamadı.");
            }
            else
            {
                GetPlaceByIdResponseDto data = new GetPlaceByIdResponseDto();
                data.PlaceId = place.PlaceId;
                data.PlaceName = place.PlaceName;
                data.PlaceAddress = place.PlaceAddress;

                return Ok(data);
            }

        }
        [HttpPost]
        public IActionResult AddPlace(AddPlaceRequestDto request)
        {
            var place = new Place
            {
                PlaceName = request.PlaceName,
                PlaceAddress = request.PlaceAddress,
            };

            context.Places.Add(place);
            context.SaveChanges();

            return Created("OK", request);

        }
        [HttpDelete("{id}")]
        public IActionResult DeletePlace(int id)
        {
            var place = context.Places.FirstOrDefault(x => x.PlaceId == id);
            if (place != null)
            {
                context.Places.Remove(place);
                context.SaveChanges();
                return Ok("Silindi");
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdatePlace(int id, [FromBody] UpdatePlaceRequestDto Place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Girilen id değerinde bir mekan mevcut değil!");
            }
            var existingPlace = context.Places.Where(p => p.PlaceId == id).FirstOrDefault<Place>();
            if (existingPlace != null)
            {
                existingPlace.PlaceName = Place.PlaceName;
                existingPlace.PlaceAddress = Place.PlaceAddress;

                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            List<GetAllPlacesResponseDto> response = context.Places.Select(x => new GetAllPlacesResponseDto
            {
                PlaceId = x.PlaceId,
                PlaceName = x.PlaceName,
                PlaceAddress = x.PlaceAddress,

            }).ToList();
            return Ok(response);
        }
    }
}
