using Microsoft.AspNetCore.DataProtection.Repositories;
using UPIWork.Models.Entity;
using UPIWork.Models.DTO;

namespace UPIWork.Repository.Service
{
    public interface BankRepository : IRepository<Bank>
    {
        Task AddBankToSystemAsync(BankDTO bankDTO);
        Task CertificateStatus(Guid bankId);
    }
}