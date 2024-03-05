namespace Calculator.Models
{
    public class CalculatorHistory
    {
        public CalculatorHistory(List<string> expressions, List<string> results)
        {
            Expressions = expressions;
            Results = results;
        }
        public List<string> Expressions { get; set; }
        public List<string> Results { get; set; }
    }
}
