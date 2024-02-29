namespace Calculator.Models
{
    public class MathExpression
    {
        public MathExpression(List<string> operations, List<string> numbers)
        {
            Operations = operations;
            Numbers = numbers;
        }
        public List<string> Operations { get; set; }
        public List<string> Numbers { get; set; }

    }
}
