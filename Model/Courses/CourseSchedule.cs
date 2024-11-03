using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Model.People;

namespace Api.Model.Courses
{
    public class CourseSchedule(Guid courseId, string location, CourseSchedule.DaysInWeekEnum day, TimeOnly beginTime, TimeOnly endTime, Guid coachId)
    {
        
        [ForeignKey("Course")]
        public Guid CourseId { get; set; } = courseId;
        public required Course Course { get; set; }

        public required Coach Coach {get; set; } 
        
        [ForeignKey("Coach")]
        public Guid CoachId { get; set; } = coachId;

        [Length(1, 50)]
        public string Location { get; set; } = location;
        public DaysInWeekEnum Day { get; set; } = day;

        public TimeOnly BeginTime { get; set; } = beginTime;
        public TimeOnly EndTime { get; set; } = endTime;

        public enum DaysInWeekEnum
        {
            M,
            Tues,
            W,
            Thurs,
            F,
            Sat,
            Sun
        }
    }
}