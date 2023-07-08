using System.ComponentModel.DataAnnotations;

public class UpdateCarDto
{
    [Required, MinLength(2), MaxLength(20)]
    public string Brand { get; set; }

    [Required, MinLength(2), MaxLength(20)]
    public string Model { get; set; }

    [Required, MinLength(2), MaxLength(20)]
    public string Color { get; set; }

    [Required, CarAge(minimumAge: 0, maximumAge: 10)]
    public DateTime ManufacturedAt { get; set; }
    public Guid OwnerId { get; set; }
}