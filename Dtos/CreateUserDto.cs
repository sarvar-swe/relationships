using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    public string Name { get; set; }
    public DateTime Birthday { get; set; }

    [Required, ValidEmail()]
    public string Email { get; set; }
    public string Username { get; set; }
}