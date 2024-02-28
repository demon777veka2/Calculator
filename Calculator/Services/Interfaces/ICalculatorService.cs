namespace Calculator.Services.Interfaces
{
    public interface ICalculatorService
    {
        string Calculate(string input);
        bool isValidationExpression(string input);
    }
}
