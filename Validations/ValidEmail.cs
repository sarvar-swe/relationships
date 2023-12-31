using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class ValidEmail : ValidationAttribute
{
    public ValidEmail()
    {
    }
    public override bool IsValid(object value)
    {
        if(value is string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov|io|uz|kz|ru)$";

            if(Regex.IsMatch(email, regex, RegexOptions.IgnoreCase) == true)
            {
                return true;
            }
            else
            {
                ErrorMessage = "Enter valid email, like someone@gmail.com";
                return false;
            }
        }
        else
        {
            ErrorMessage = "Email should be string";
            return false;
        }
    }
}