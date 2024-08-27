using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UPIWork.Models.DTO;
using UPIWork.Repository.Service;

namespace UPIWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Endpoint to register a new customer
        [HttpPost("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Customer data is null.");
            }

            await _customerRepository.AddCustomerAsync(customerDto);
            return StatusCode(StatusCodes.Status201Created, "Customer registered successfully.");
        }

        // Endpoint to get all customers
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }

        // Endpoint to get a customer by their ID
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound($"Customer with ID {customerId} not found.");
            }
            return Ok(customer);
        }

        // Endpoint to update a customer's details
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, [FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Customer data is null.");
            }

            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound($"Customer with ID {customerId} not found.");
            }

            await _customerRepository.UpdateCustomerDetails(customerId, customerDto);
            return Ok("Customer details updated successfully.");
        }
    }
}
