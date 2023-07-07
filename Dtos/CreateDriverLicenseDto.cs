using System.ComponentModel.DataAnnotations;

public class CreateDriverLicenseDto
{
    [Required, MinLength(12)]
    public string Serial { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}