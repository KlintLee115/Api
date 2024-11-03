using Api.Model.Courses;
using Api.Model.People;

namespace Api.Model
{
    public class Coach(string name, string email, string password, string phoneNumber) : Account(name, email, password, phoneNumber)
    {
        public List<Course> Courses {get; set;} = [];
        public void ManageBookings() {

        }

        public void ManageTeams() {
            
        }
    }
}