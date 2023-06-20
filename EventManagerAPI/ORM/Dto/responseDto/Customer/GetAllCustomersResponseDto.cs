namespace EventManagerAPI.ORM.Dto.responseDto.Customer
{
    public class GetAllCustomersResponseDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public int CustomerAge { get; set; }
    }
}
