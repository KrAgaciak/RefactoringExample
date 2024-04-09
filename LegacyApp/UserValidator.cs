using System;
namespace LegacyApp;

public class UserValidator: UserValidatorGeneric
{
    protected const int AGE = 21;
    
    
    //need for error feedback 
    public override bool UserValidation(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        if (!EmailValidation(email))
        {
            return false;
        }

        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < AGE)
        {
            return false;
        }

        return true;
    }

    public override bool EmailValidation(string email)
    {
        if (!email.Contains("@") || !email.Contains(".")) // zmian w stosunku do orginalnej procedury
        {
            return false;
        }
        else
        {
            return true;   
        }
    }
}