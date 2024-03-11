using Calculator.Models;

namespace Calculator.Classes.Interfaces
{
    public interface IStorage
    {
        public List<CalculatorSolution> GetCalculatorSolutions();
        public void AddCalculatorSolution(string expression, string result);
    }
}
