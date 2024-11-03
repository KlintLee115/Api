using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Api.Model.People;

namespace Api.Model.Courses
{
    public class Course(string name, DateOnly startDateTime, DateOnly endDateTime, Guid coachId)
    {
        [Key]
        public Guid Id {get; init; }
        
        [Length(1, 50)]
        public string Name {get; set; } = name;
        public string Description {get; set; } = string.Empty;
        
        public required Coach Coach {get; set; } 
        
        public Guid CoachId { get; set; } = coachId;
        public DateOnly StartDateTime {get; set; } = startDateTime;
        public DateOnly EndDateTime {get; set; } = endDateTime;
        
        public Collection<Customer> Customers {get; set; } = [];
        
        public void DepositCredit() {

        }

        public void SignWaiver() {
            
        }
    }
}