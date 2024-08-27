using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UPIWork.Models.Data;
using UPIWork.Models.Entity;
using UPIWork.Models.DTO;
using UPIWork.Repository.Service;

namespace UPIWork.Repository.Helper
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        private static readonly Random _random = new Random();

        public async Task AddCustomerAsync(CustomerDTO customerDTO)
        {
            // Create a new Customer entity, generate a new CustomerId
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),  // Generated internally
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber,
                Gender = customerDTO.Gender,
                Nationality = customerDTO.Nationality,
                BankId = customerDTO.BankId
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();


            var account = new Account
            {
                AccountNumber = GenerateAccountNumber(),
                Balance = 0, // Initial balance can be set here
                CustomerId = customer.CustomerId,
                BankId = customerDTO.BankId,
                CardNumber = GenerateCardNumber(),
                CardExpiryDate = DateTime.Now.AddYears(5),
                CVv = GenerateCVV(),
            };

            // Fetch the bank name using the BankId
            var bank = await _dbContext.Banks.FindAsync(customerDTO.BankId);
            if (bank == null)
            {
                throw new Exception("Bank not found");
            }

            // Generate UPI ID
            string upiId = GenerateUPIID(customer.Name, account.AccountNumber);
            account.UPIID = upiId; // Assign the generated UPI ID

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();
        }

        private string GenerateAccountNumber()
        {
            // Generate a random 10-digit account number
            Random random = new Random();
            string accountNumber = random.Next(10000, 99999).ToString() + random.Next(10000, 99999).ToString();
            return accountNumber;
        }


        public string GenerateCardNumber()
        {
            // Generate a random 12-digit card number
            Random random = new Random();
            long minValue = 100000000000; // 12-digit minimum
            long maxValue = 999999999999; // 12-digit maximum

            long cardNumber = random.NextInt64(minValue, maxValue + 1);
            return cardNumber.ToString();
        }


        public string GenerateCVV()
        {
            Random random = new Random();
            return random.Next(100, 999).ToString();
        }

        public async Task<CustomerDTO> GetCustomerByCustomerId(Guid customerId)
        {
            return await _dbContext.Customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerDTO
                {
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Gender = c.Gender,
                    Nationality = c.Nationality,
                    BankId = c.BankId
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateCustomerDetails(Guid customerId, CustomerDTO customerDTO)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.Name = customerDTO.Name;
                customer.Email = customerDTO.Email;
                customer.PhoneNumber = customerDTO.PhoneNumber;
                customer.Gender = customerDTO.Gender;
                customer.Nationality = customerDTO.Nationality;

                _dbContext.Customers.Update(customer);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerDTO>> GetAll()
        {
            return await _dbContext.Customers
                .Select(c => new CustomerDTO
                {
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Gender = c.Gender,
                    Nationality = c.Nationality,
                    BankId = c.BankId
                })
                .ToListAsync();
        }

        public string GenerateUPIID(string Name, string accountNumber)
        {
            // Get the last 4 digits of the account number
            string lastFourDigits = accountNumber.Substring(accountNumber.Length - 4);

            // Convert the customer's name to lowercase and remove any spaces or special characters
            string cleanedName = Name.ToLower().Replace(" ", "");

            // Format the UPI ID to end with @paytm
            string upiId = $"{cleanedName}{lastFourDigits}@paytm";

            return upiId;
        }




    }
}
