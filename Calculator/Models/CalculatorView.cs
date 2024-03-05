namespace Calculator.Models
{
    public class CalculatorView
    {
        public CalculatorView(string expression, string result, CalculatorHistory calculatorHistory, string? error)
        {
            Expression = expression;
            Result = result;
            History = calculatorHistory;
            Error = error;
        }
        public string Expression { get; set; }
        public string Result { get; set; }
        public CalculatorHistory History { get; set; }
        public string? Error { get; set; }

    }
}
