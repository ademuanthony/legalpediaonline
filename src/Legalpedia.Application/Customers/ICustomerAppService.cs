using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Customers.Dto;

namespace Legalpedia.Customers
{
    public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, int, PagedResultRequestDto, 
        CreateCustomerDto, UpdateCustomerDto>
    {
    }
}
