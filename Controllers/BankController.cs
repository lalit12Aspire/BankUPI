using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UPIWork.Models.Data;
using UPIWork.Models.Entity;
using UPIWork.Models.DTO;

namespace UPIWork.Controllers
{

    [Route("api/Controller")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BankController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("RegisterBank")]
        //       [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterBank([FromBody] BankDTO bankDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!bankDTO.IsCertified)
            {
                return BadRequest("Cannot proceed with registration as the bank is not certified.");
            }

            if (ModelState.IsValid)
            {
                var bank = new Bank
                {
                    BankId = Guid.NewGuid(),
                    BankName = bankDTO.Name,
                    Address = bankDTO.Address,
                    BankType = bankDTO.BankType,
                    IsCertified = bankDTO.IsCertified,
                    NationalBank = bankDTO.IsNationalized
                };
                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Bank Registered Successfully!" });
            }

            return BadRequest(ModelState);
        }
    }

}