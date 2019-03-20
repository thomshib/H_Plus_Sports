using H_Plus_Sports.Contracts;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;   

namespace H_Plus_Sports.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly H_Plus_SportsContext _context;
       

        public CustomerRepository(H_Plus_SportsContext context)
        {
            _context = context;
            
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;

        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Find(int id)
        {
            var customer = await _context.Customer.SingleAsync(c => c.CustomerId == id);
            return customer;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer;
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleAsync(c => c.CustomerId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;

        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
