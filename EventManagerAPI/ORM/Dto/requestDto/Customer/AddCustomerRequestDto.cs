namespace EventManagerAPI.ORM.Dto.requestDto.Customer
{
    public class AddCustomerRequestDto
    {
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public int CustomerAge { get; set; }
    }
}
