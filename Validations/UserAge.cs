using System.ComponentModel.DataAnnotations;

public class UserAge : ValidationAttribute
{
    private readonly int minimumAge;
    private readonly int maximumAge;

    public UserAge(int minimumAge, int maximumAge)
    {
        this.minimumAge = minimumAge;
        this.maximumAge = maximumAge;
    }

    public override bool IsValid(object value)
    {
        if(value is DateTime birthday)
        {
            var age = (DateTime.Now - birthday).TotalDays / 365;
            
            var isAgeValid = age >= minimumAge && age <= maximumAge; 
            if(isAgeValid)
                return true;
            else 
            {
                ErrorMessage = $"User age should be between {minimumAge} and {maximumAge}";
                return false;
            }
        }
        else
        {
            ErrorMessage = "User birth year should be DateTime type.";
            return false;
        }
    }
}