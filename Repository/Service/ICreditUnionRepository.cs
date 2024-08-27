using System;
using System.Threading.Tasks;
using UPIWork.Models;
using UPIWork.Models.Entity;
using UPIWork.Repository.Service;
using UPIWork.Models.DTO;

namespace UnifiedPaymentSystem.Repository.Service
{
    public interface ICreditUnionRepository : IRepository<CreditUnion>
    {
        Task AddCreditUnionAsync(CreditUnionDTO creditUnionDto);
        Task<CreditUnion> VerifyCertificationAsync(Guid creditUnionId);
    }

}
