using System;

namespace LegacyApp;

public abstract class UserValidatorGeneric
{
    public abstract bool UserValidation(string firstName, string lastName, string email, DateTime dateOfBirth,
        int clientId);

    public abstract bool EmailValidation(string email);


}