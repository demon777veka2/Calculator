namespace Calculator.Models
{
    public class CalculatorOutput
    {
        public string Expression { get; set; }
        public string Result { get; set; } = "";
        public List<CalculatorSolution> CalculatorSolutions { get; set; }
        public string? Error { get; set; } = null;

    }
}
