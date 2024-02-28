using Calculator.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private const string regexExpression = @"^(-{0,1}[\d,]+[+-/*])+[\d,]+$";

        public string Calculate(string input)
        {
            return plusMinus(multDiv(calculateInBracket(input)));
        }

        public bool isValidationExpression(string input)
        {
            Regex regex = new Regex(regexExpression);
            string inputWithoutBracket = input;

            if (input == null)
                return false;

            if (input.Contains('('))
            {
                int countBracket = input.Count(x => x == '(');

                for (int i = 0; i < countBracket; i++)
                {
                    int firstOpenBracket = inputWithoutBracket.IndexOf('(') + 1;
                    int firstCloseBracket = inputWithoutBracket.IndexOf(')');

                    string expression = inputWithoutBracket.Remove(firstCloseBracket, inputWithoutBracket.Length - firstCloseBracket);
                    expression = expression.Remove(0, firstOpenBracket);

                    if (!regex.IsMatch(expression))
                    {
                        return false;
                    }

                    inputWithoutBracket = inputWithoutBracket.Replace("(" + expression + ")", "0");
                }
            }

            if (regex.IsMatch(inputWithoutBracket))
            {
                return true;
            }

            return false;
        }

        private string calculateInBracket(string input)
        {
            if (!input.Contains('('))
                return input;

            int countBracket = input.Count(x => x == '(');
            string inputWithoutBracket = input;

            for (int i = 0; i < countBracket; i++)
            {
                int firstOpenBracket = inputWithoutBracket.IndexOf('(') + 1;
                int firstCloseBracket = inputWithoutBracket.IndexOf(')');

                string expression = inputWithoutBracket.Remove(firstCloseBracket, inputWithoutBracket.Length - firstCloseBracket);
                expression = expression.Remove(0, firstOpenBracket);

                string resultExpression = plusMinus(multDiv(expression));

                inputWithoutBracket = inputWithoutBracket.Replace("(" + expression + ")", resultExpression);
            }

            return inputWithoutBracket;
        }

        private string multDiv(string input)
        {
            if (!input.Contains('*') & !input.Contains('/'))
                return input;

            Regex regexOperation = new Regex(@"[+-/*]");
            Regex regexNumber = new Regex(@"[\d,]+");

            List<string> operations = regexNumber.Split(input).ToList();
            operations.RemoveAll(x => x == "");


            List<string> numbers = regexOperation.Split(input).ToList();
            numbers.RemoveAll(x => x == "");

            if (input[0] == '-')
            {
                numbers[0] = operations[0] + numbers[0];
                operations.RemoveAt(0);
            }

            for (int i = 0; i < operations.Count(); i++)
            {
                if (operations[i] != "+" && operations[i] != "-")
                {
                    string calculatingNumbers;

                    if (operations[i] == "*")
                    {
                        calculatingNumbers = (Math.Round(Convert.ToDecimal(numbers[i]) * Convert.ToDecimal(numbers[i + 1]), 3)).ToString();
                    }
                    else
                    {
                        calculatingNumbers = (Math.Round(Convert.ToDecimal(numbers[i]) / Convert.ToDecimal(numbers[i + 1]), 3)).ToString();
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
            Regex regexNumber = new Regex(@"[\d,]+");

            List<string> operations = regexNumber.Split(input).ToList();
            operations.RemoveAll(x => x == "");

            List<string> numbers = regexOperation.Split(input).ToList();
            numbers.RemoveAll(x => x == "");

            if (input[0] == '-')
            {
                numbers[0] = operations[0].ToString() + numbers[0].ToString();
                operations.RemoveAt(0);
            }

            for (int i = 0; i < operations.Count(); i++)
            {
                string calculatingNumbers;

                if (operations[i] == "+")
                {
                    calculatingNumbers = (Math.Round(Convert.ToDecimal(numbers[i]) + Convert.ToDecimal(numbers[i + 1]), 3)).ToString();
                }
                else
                {
                    calculatingNumbers = (Math.Round(Convert.ToDecimal(numbers[i]) - Convert.ToDecimal(numbers[i + 1]), 3)).ToString();
                }
                operations[i] = "";
                numbers[i] = "";
                numbers[i + 1] = calculatingNumbers;
            }

            return numbers.Where(x => x != "").First().ToString();
        }
    }
}