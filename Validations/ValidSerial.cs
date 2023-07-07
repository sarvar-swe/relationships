using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class ValidSerial : ValidationAttribute
{
    public ValidSerial()
    {
    }

    public override bool IsValid(object value)
    {
        if(value is string serail)
        {
            if(serail is null)
                return false;

            string regex = "^(([A-Z]{2}[0-9]{2})"
                            + "( )|([A-Z]{2}-[0-9]"
                            + "{2}))((19|20)[0-9]"
                            + "[0-9])[0-9]{7}$";

            if(Regex.IsMatch(serail, regex, RegexOptions.IgnoreCase) == true)
            {
                return true;
            }
            else
            {
                ErrorMessage = "Enter valid serail";
                return false;
            }
        }
        else
        {
            ErrorMessage = "Serail should be string";
            return false;
        }
    }
}