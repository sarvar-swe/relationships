using System.ComponentModel.DataAnnotations;

public class CreateCourseDto
{
    [Required, MinLength(2), MaxLength(20)]
    public string Name { get; set; } 
    public DateTime StartDate { get; set; }
    public int DurationInMonths { get; set; }
    public decimal Price { get; set; }
}
