namespace EventManagerAPI.ORM.Dto.requestDto.Event
{
    public class UpdateEventRequestDto
    {
        public string EventTitle { get; set; }
        public string EventPerson { get; set; }
        public string EventCategory { get; set; }
        public string EventDescription { get; set; }
        public int PlaceId { get; set; }
        public DateTime EventStartingDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsEventPaid { get; set; }
        public decimal? EventPrice { get; set; }
        public string? EventImageUrlOne { get; set; }
        public string? EventImageUrlTwo { get; set; }
        public string? EventImageUrlThree { get; set; }
        public string? EventCity { get; internal set; }
    }
}
