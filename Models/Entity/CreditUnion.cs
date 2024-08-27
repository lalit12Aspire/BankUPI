using System.ComponentModel.DataAnnotations;

namespace UPIWork.Models.Entity
{
    public class CreditUnion : FinancialInstitution
    {
        [Key]
        public  Guid CreditUnionId{get;set;}
        public int NumberOfMembers { get; set; }
        public bool IsMemberOwned{get;set;}
        public string RegulatoryAgency{get;set;}
        public decimal TotalDeposits{get;set;}
        public string CreditUnionType{get;set;
        }

    }
}