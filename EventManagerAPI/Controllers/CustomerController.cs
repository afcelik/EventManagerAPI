using EventManagerAPI.ORM.Context;
using EventManagerAPI.ORM.Dto.requestDto.Customer;
using EventManagerAPI.ORM.Dto.responseDto.Customer;
using EventManagerAPI.ORM.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        EventContext context = new EventContext();

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            List<GetAllCustomersResponseDto> data = context.Customers.Select(x => new GetAllCustomersResponseDto()
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                CustomerEmail = x.CustomerEmail,
                CustomerPhone = x.CustomerPhone,
                CustomerCity = x.CustomerCity,
                CustomerAge = x.CustomerAge,

            }).ToList();


            return Ok(data);

        }
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {

            var customer = context.Customers.FirstOrDefault(x => x.CustomerId == id);

            if (customer == null)
            {
                return NotFound(id + "Girilen idye sahip müşteri bulunamadı.");
            }
            else
            {
                GetCustomerByIdResponseDto data = new GetCustomerByIdResponseDto();
                data.CustomerId = customer.CustomerId;
                data.CustomerName = customer.CustomerName;
                data.CustomerSurname = customer.CustomerSurname;
                data.CustomerPhone = customer.CustomerPhone;
                data.CustomerCity = customer.CustomerCity;
                data.CustomerAge = customer.CustomerAge;

                return Ok(data);
            }

        }
        [HttpPost]
        public IActionResult AddCustomer(AddCustomerRequestDto request)
        {
            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                CustomerSurname = request.CustomerSurname,
                CustomerEmail = request.CustomerEmail.Trim(),
                CustomerPhone = request.CustomerPhone.Trim(),
                CustomerCity = request.CustomerCity,
                CustomerAge = request.CustomerAge
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            return Created("OK", request);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
                return Ok("Silindi");
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerRequestDto Customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Girilen id değerinde bir müşteri mevcut değil!");
            }
            var existingCustomer = context.Customers.Where(p => p.CustomerId == id).FirstOrDefault<Customer>();
            if (existingCustomer != null)
            {
                existingCustomer.CustomerName = Customer.CustomerName;
                existingCustomer.CustomerSurname = Customer.CustomerSurname;
                existingCustomer.CustomerEmail = Customer.CustomerEmail;
                existingCustomer.CustomerPhone = Customer.CustomerPhone;
                existingCustomer.CustomerCity = Customer.CustomerCity;
                existingCustomer.CustomerAge = Customer.CustomerAge;

                context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            List<GetAllCustomersResponseDto> response = context.Customers.Select(x => new GetAllCustomersResponseDto
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                CustomerEmail = x.CustomerEmail,
                CustomerPhone = x.CustomerPhone,
                CustomerCity = x.CustomerCity,
                CustomerAge = x.CustomerAge,

            }).ToList();
            return Ok(response);
        }
    }
}
