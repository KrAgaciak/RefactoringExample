using System;

namespace LegacyApp
{
    public class UserService
    {
        private UserServiceScoreCalculatorGeneric userServiceScoreCalculator;
        private UserValidatorGeneric userValidator;
        private ClientRepositoryGeneric clientRepository;

        
        public UserService()
        {
            userServiceScoreCalculator = new UserServiceScoreCalculator();
            userValidator = new UserValidator();
            clientRepository = new ClientRepository();
        }
        
        public UserService(UserServiceScoreCalculatorGeneric _userServiceScoreCalculatorGeneric, 
                            UserValidatorGeneric _userValidator,
                            ClientRepositoryGeneric _clientRepository)
        {
            userServiceScoreCalculator = _userServiceScoreCalculatorGeneric;
            userValidator = _userValidator;
            clientRepository = _clientRepository;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!userValidator.UserValidation(firstName, lastName, email, dateOfBirth, clientId))
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
