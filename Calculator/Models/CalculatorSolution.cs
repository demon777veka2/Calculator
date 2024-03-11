namespace Calculator.Models
{
    public class CalculatorSolution
    {
        public CalculatorSolution(string expression, string result) 
        { 
            Expression = expression;
            Result = result;
        }
        public string Expression { get; set; }
        public string Result { get; set; }
    }
}
