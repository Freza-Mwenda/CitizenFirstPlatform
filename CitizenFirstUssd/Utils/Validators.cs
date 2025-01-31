using System.Text.RegularExpressions;

namespace CitizenFirstUssd.Utils;

public static class Validators
{
    public static bool IsValidNrc(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        
        string pattern = @"^(\d{9}|(\d{6}/\d{2}/\d))$";
        return Regex.IsMatch(input, pattern);
    }

    public static string FormatNrc(this string input)
    {
        if (input.Contains("/"))
        {
            return input; 
        }
        
        if (input.Length == 9)
        {
            return input.Insert(6, "/").Insert(9, "/");
        }
        
        throw new ArgumentException("Input must be exactly 9 characters long without slashes.");
    }

}