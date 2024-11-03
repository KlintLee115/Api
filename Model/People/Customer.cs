namespace Api.Model.People
{
    public class Customer: Account
    {
        public Customer(string name, string email, string password, string phoneNumber) : base(name, email, password, phoneNumber) {

        }

        public Customer(string name, string email, string password, string phoneNumber, Guid familyId, RolesEnum role) : base(name, email, password, phoneNumber) {
            FamilyId = familyId;
            Role = role;
        }
        public bool HasConsentMarketingEmails { get; set; }
        public bool HasConsentMarketingSms { get; set; }
        public bool ShouldReceiveReceiptsForAllPayments { get; set; }
        public ICollection<FinancialInfo> FinancialInfos { get; set; } = [];
        public AthleteInfo? AthleteInfo { get; set; }
        public Guid? FamilyId { get; set; }
        public Family? Family { get; set; }
        public RolesEnum? Role { get; set; }
        public enum RolesEnum
        {
            Child,
            Parent
        }
    }
}
