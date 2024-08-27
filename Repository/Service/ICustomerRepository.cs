using System.Collections.Generic;
using System.Threading.Tasks;
using UPIWork.Models.Entity;
using UPIWork.Models.DTO;

namespace UPIWork.Repository.Service
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddCustomerAsync(CustomerDTO customerDTO);
        Task<CustomerDTO> GetCustomerByCustomerId(Guid customerId);
        Task UpdateCustomerDetails(Guid customerId, CustomerDTO customerDTO);
    }
}
