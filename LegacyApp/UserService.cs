using System;

namespace LegacyApp
{
    public class UserService: UserServiceGeneric
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!UserValidation(firstName, lastName, email, dateOfBirth, clientId))
            {
                return false;
            }
            
            try
            {
                var client = clientRepository.GetById(clientId);
                
                var user = new User
                {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                userServiceScoreCalculator.UserCreditScoreCalculation(user);
                
                if (userServiceScoreCalculator.UserCreditScoreValidation(user))
                {
                    UserDataAccess.AddUser(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return false; //zmiana działania aplikacji, nie wywala całego programu przy nieistniejącym kliencie :)
            }
        }
    }
}
