namespace LegacyApp;

public class UserServiceScoreCalculator: UserServiceScoreCalculatorGeneric
{
    protected const int CREDITLIMIT = 500;
    protected const string CLIENTTYPE_VERYIMPORTANT = "VeryImportantClient";
    protected const string CLIENTTYPE_IMPORTANT = "ImportantClient";

    public override void UserCreditScoreCalculation(User user)
    {
        Client client = user.Client;
            
        if (client.Type == CLIENTTYPE_VERYIMPORTANT)
        {
            user.HasCreditLimit = false;
        }
        else
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
                
            if (client.Type == CLIENTTYPE_IMPORTANT)
            {
                user.CreditLimit = user.CreditLimit*2;
            }
            else
            {
                user.HasCreditLimit = true;
            }
        }
    }

    public override bool UserCreditScoreValidation(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < CREDITLIMIT)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}