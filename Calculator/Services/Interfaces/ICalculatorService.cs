namespace Calculator.Services.Interfaces
{
    public interface ICalculatorService
    {
        int Calculate(string input);
        bool isValidationExpression(string expression);
    }
}
