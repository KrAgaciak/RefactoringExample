using System;
namespace LegacyApp;

public abstract class UserServiceGeneric
{
    protected ClientRepository clientRepository = new ClientRepository();
    protected const int AGE = 21;
    protected UserServiceScoreCalculatorGeneric userServiceScoreCalculator = new UserServiceScoreCalculator(); 
    
    protected bool UserValidation(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
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

    protected bool EmailValidation(string email)
    {
        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }
        else
        {
            return true;   
        }
    }
}