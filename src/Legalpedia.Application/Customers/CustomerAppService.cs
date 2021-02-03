using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Legalpedia.Customers;
using Legalpedia.Customers.Dto;
using Legalpedia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Legalpedia.Customers
{
    [IgnoreAntiforgeryToken]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, int,
        PagedResultRequestDto, CreateCustomerDto, UpdateCustomerDto>, ICustomerAppService
    {
        private IRepository<Customer> _customeRepository;

        public CustomerAppService(IRepository<Customer> customeRepository) : base(customeRepository)
        {
            _customeRepository = customeRepository;
        }

        public override Task<CustomerDto> CreateAsync(CreateCustomerDto input)
        {
            if (Repository.GetAll().Any(c=>c.PhoneNumber == input.PhoneNumber))
            {
                throw new UserFriendlyException("The selected phone number is already in use");
            }
            if (Repository.GetAll().Any(c=>c.Email == input.Email))
            {
                throw new UserFriendlyException("The selected email is already in use");
            }
            
            if (input.DateOfBirth.Year == new DateTime().Year)
            {
                input.DateOfBirth = DateTime.Now.AddYears(-25);
            }
            return base.CreateAsync(input);
        }
    }
}
