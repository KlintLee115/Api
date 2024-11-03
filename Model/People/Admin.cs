namespace Api.Model.People
{
    public class Admin(string name, string email, string password, string phoneNumber) : Account(name, email, password, phoneNumber)
    {
        public void ManageBookings() {

        }

        public void ManageFacilities() {
            
        }

        public void ManageCourses() {
            
        }
    }
}