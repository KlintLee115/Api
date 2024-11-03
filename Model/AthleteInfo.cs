using System.ComponentModel.DataAnnotations;
using Api.Model.People;

namespace Api.Model;

public class AthleteInfo(Guid customerId)
{
    public required Customer Customer { get; set; }
    
    [Key]
    public Guid CustomerId { get; set; } = customerId;
    
    public int? Age { get; set; }
    public double? Height { get; set; }
    public double? Weight { get; set; }
    public DateOnly? Dob { get; set; }
    public char? Gender { get; set; }

    private int? Attendance {get; set;}
    private double? Ppg {get; set;}
    private double? Rpg {get; set;}
}