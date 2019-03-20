using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H_Plus_Sports.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

       
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            
            _customerRepository = customerRepository;
        }

        private async Task<bool> CustomerExists(int id)
        {
            return await _customerRepository.Exists(id);
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            return new ObjectResult(_customerRepository.GetAll());
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id )
        {
            var customer = await _customerRepository.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _customerRepository.Add(customer);
            
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id,[FromBody] Customer customer)
        {
            await _customerRepository.Update(customer);
            return Ok(customer);
           
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if(!await CustomerExists(id))
            {
                return NotFound();
            }
            await _customerRepository.Remove(id);
            return Ok();
        }


    }
}