using Api.Model.People;

namespace Api.Model
{
    public class Family
    {
        public Guid Id { get; init; }
        public List<Customer> Members { get; } = [];
    }
}