using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Model.People;

namespace Api.Model
{
    public class FinancialInfo()
    {
        [Key]
        public Guid Id {get; init;}
        public ICollection<Customer> Customers { get; set; } = [];

        public int BankAccountNumber { get; set; }
        public int Credit { get; set; } = 0;
        public int Balance { get; set; } = 0;
    }
}