using Calculator.Models;

namespace Calculator.Classes.Interfaces
{
    public interface IStorage
    {
        public CalculatorHistory getCalculatorHistory();
        public void addCalculatorHistory(string expression, string result);
    }
}
