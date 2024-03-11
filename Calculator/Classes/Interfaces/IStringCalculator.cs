namespace Calculator.Services.Interfaces
{
    public interface IStringCalculator
    {
        string Calculate(string input);
        bool IsCorrectExpression(string input);
    }
}
