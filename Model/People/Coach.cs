using Api.Model.Courses;

namespace Api.Model.People
{
    public class Coach(string name, string email, string password, string phoneNumber) : Account(name, email, password, phoneNumber)
    {
        public ICollection<CourseSchedule> CourseSchedules { get; set; } = [];
        public void ManageBookings()
        {

        }

        public void ManageTeams()
        {

        }
    }
}