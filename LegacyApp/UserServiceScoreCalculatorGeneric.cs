namespace LegacyApp;

public abstract class UserServiceScoreCalculatorGeneric
{
    public abstract void UserCreditScoreCalculation(User user);
    
    public abstract bool UserCreditScoreValidation(User user);
}