using Calculator.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System.Text.RegularExpressions;

namespace Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Calculate(string input)
        {
            string result = multDiv(input);
            return Convert.ToInt32(plusMinus(result));
        }

        public bool isValidationExpression(string expression)
        {
            Regex regex = new Regex(@"^([\d]+[+-/*])+[\d]+$");
            
            if (regex.IsMatch(expression))
            {
                return true;
            }

            return false;
        }

        private string multDiv(string input)
        {
            if (!input.Contains('*') & !input.Contains('/'))
                return input;

            Regex regexOperation = new Regex(@"[+-/*]");
            Regex regexNumber = new Regex(@"[\d]+");

            List<string> operations = regexNumber.Split(input).ToList();
            operations.Remove("");
            operations.Remove("");

            List<string> numbers = regexOperation.Split(input).ToList();

            for (int i = 0; i < operations.Count(); i++)
            {
                if (operations[i] != "+" && operations[i] != "-")
                {
                    string calculatingNumbers;

                    if (operations[i] == "*")
                    {
                        calculatingNumbers = (Convert.ToInt32(numbers[i]) * Convert.ToInt32(numbers[i + 1])).ToString();
                    }
                    else
                    {
                        calculatingNumbers = (Convert.ToInt32(numbers[i]) / Convert.ToInt32(numbers[i + 1])).ToString();
                    }
                    operations[i] = "";
                    numbers[i] = "";
                    numbers[i + 1] = calculatingNumbers;
                }
            }

            operations = operations.Where(x => x != "").ToList();
            numbers = numbers.Where(x => x != "").ToList();

            if (numbers.Count() > 1)
            {
                string newExpression = "";

                for (int i = 0; i < operations.Count(); i++)
                {
                    newExpression += numbers[i] + operations[i];
                }

                newExpression += numbers[numbers.Count - 1];

                return newExpression;
            }

            return numbers[0].ToString();
        }

        private string plusMinus(string input)
        {
            if (!input.Contains('+') && !input.Contains('-'))
                return input;

            Regex regexOperation = new Regex(@"[+-]");
            Regex regexNumber = new Regex(@"[\d]+");

            List<string> operations = regexNumber.Split(input).ToList();
            operations.Remove("");
            operations.Remove("");

            List<string> numbers = regexOperation.Split(input).ToList();

            for (int i = 0; i < operations.Count(); i++)
            {
                string calculatingNumbers;

                if (operations[i] == "+")
                {
                    calculatingNumbers = (Convert.ToInt32(numbers[i]) + Convert.ToInt32(numbers[i + 1])).ToString();
                }
                else
                {
                    calculatingNumbers = (Convert.ToInt32(numbers[i]) - Convert.ToInt32(numbers[i + 1])).ToString();
                }
                operations[i] = "";
                numbers[i] = "";
                numbers[i + 1] = calculatingNumbers;
            }

            return numbers.Where(x => x != "").First().ToString();
        }
    }
}