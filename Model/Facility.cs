namespace Api.Model
{
    public class Facility(string location, string name)
    {
        public required Guid Id {get; init;}
        
        public string Location { get; set; } = location;
        public string Name { get; set; } = name;
    }
}