using System.ComponentModel.DataAnnotations;

public class UpdateCourseDto
{
    [Required, MinLength(2), MaxLength(20)]
    public string Name { get; set; }

    [Required] 
    public DateTime StartDate { get; set; }

    [Required, Range(10, 20)]
    public int DurationInMonths { get; set; }

    [Required, Range(300, 900)]
    public decimal Price { get; set; }
}
