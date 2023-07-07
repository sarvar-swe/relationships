using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
{
    [Required, MinLength(2), MaxLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Date is reuired"), UserAge(minimumAge: 18, maximumAge: 70)]
    public DateTime Birthday { get; set; }
    
    [Required, ValidEmail(), MinLength(12), MaxLength(50)]
    public string Email { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public string Username { get; set; }
}