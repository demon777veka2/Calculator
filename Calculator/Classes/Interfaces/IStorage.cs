using Calculator.Models;

namespace Calculator.Classes.Interfaces
{
    public interface IStorage
    {
        public List<CalculatorSolution> getCalculatorSolutions();
        public void addCalculatorSolution(string expression, string result);
    }
}
