using Calculator.Services.Interfaces;
using System.Data;
using System.Text.RegularExpressions;
using Calculator.Models;

namespace Calculator.Services
{
    public class StringCalculator : IStringCalculator
    {
        private const string regexExpression = @"^(-{0,1}[\d,]+[+-/*])+-{0,1}[\d,]+$";

        public string Calculate(string input)
        {
            return calculationPlusMinus(сalculationMultiplicationDivision(calculateInBracket(input)));
        }

        public bool isCorrectExpression(string input)
        {
            Regex regex = new Regex(regexExpression);
            string inputWithoutBracket = input;

            if (input == null || inputWithoutBracket.Count(x => x == '(') != inputWithoutBracket.Count(x => x == ')'))
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

        private MathExpression splittingAnExpression(string input)
        {
            Regex regexNumber = new Regex(@"[\d,.]");
            Regex regexOperation = new Regex(@"[-+*/]");

            List<string> operations = new List<string> { };
            List<string> numbers = new List<string> { };

            int indexNumber = 0;
            int indexOperation = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[0] == '-' && i == indexNumber)
                {
                    numbers.Add("-");
                    i++;
                }

                if (regexNumber.IsMatch(input[i].ToString()))
                {
                    if (numbers.Count() - 1 == indexNumber)
                    {
                        numbers[indexNumber] += input[i];
                    }
                    else
                    {
                        numbers.Add(input[i].ToString());
                    }
                }

                if (regexOperation.IsMatch(input[i].ToString()))
                {
                    if (input[i] == '-' && regexOperation.IsMatch(input[i - 1].ToString()))
                    {
                        if (numbers.Count() - 1 == indexNumber)
                        {
                            numbers[indexNumber] = input[i].ToString();
                        }
                        else
                        {
                            numbers.Add(input[i].ToString());
                        }
                    }
                    else
                    {
                        operations.Add(input[i].ToString());
                        indexOperation++;
                        indexNumber++;
                    }
                }
            }

            return new MathExpression(operations, numbers);
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

                string resultExpression = calculationPlusMinus(сalculationMultiplicationDivision(expression));

                inputWithoutBracket = inputWithoutBracket.Replace("(" + expression + ")", resultExpression);
            }

            return inputWithoutBracket;
        }

        private string сalculationMultiplicationDivision(string input)
        {
            if (!input.Contains('*') & !input.Contains('/'))
                return input;

            MathExpression splitExpression = splittingAnExpression(input);

            for (int i = 0; i < splitExpression.Operations.Count(); i++)
            {
                if (splitExpression.Operations[i] != "+" && splitExpression.Operations[i] != "-")
                {
                    string calculatingNumbers;

                    if (splitExpression.Operations[i] == "*")
                    {
                        calculatingNumbers = (Math.Round(Convert.ToDouble(splitExpression.Numbers[i]) * Convert.ToDouble(splitExpression.Numbers[i + 1]), 3)).ToString();
                    }
                    else
                    {
                        calculatingNumbers = (Math.Round(Convert.ToDouble(splitExpression.Numbers[i]) / Convert.ToDouble(splitExpression.Numbers[i + 1]), 3)).ToString();
                    }
                    splitExpression.Operations[i] = "";
                    splitExpression.Numbers[i] = "";
                    splitExpression.Numbers[i + 1] = calculatingNumbers;
                }
            }

            splitExpression.Operations = splitExpression.Operations.Where(x => x != "").ToList();
            splitExpression.Numbers = splitExpression.Numbers.Where(x => x != "").ToList();

            if (splitExpression.Numbers.Count() > 1)
            {
                string newExpression = "";

                for (int i = 0; i < splitExpression.Operations.Count(); i++)
                {
                    newExpression += splitExpression.Numbers[i] + splitExpression.Operations[i];
                }

                newExpression += splitExpression.Numbers[splitExpression.Numbers.Count - 1];

                return newExpression;
            }

            return splitExpression.Numbers[0].ToString();
        }

        private string calculationPlusMinus(string input)
        {
            int countOperation = input.Where(x => x == '+' || x == '-').Count();

            if (countOperation == 0 || (countOperation == 1 && input[0] == '-'))
                return input;

            MathExpression splitExpression = splittingAnExpression(input);

            for (int i = 0; i < splitExpression.Operations.Count(); i++)
            {
                string calculatingNumbers;

                if (splitExpression.Operations[i] == "+")
                {
                    calculatingNumbers = (Math.Round(Convert.ToDouble(splitExpression.Numbers[i]) + Convert.ToDouble(splitExpression.Numbers[i + 1]), 3)).ToString();
                }
                else
                {
                    calculatingNumbers = (Math.Round(Convert.ToDouble(splitExpression.Numbers[i]) - Convert.ToDouble(splitExpression.Numbers[i + 1]), 3)).ToString();
                }
                splitExpression.Operations[i] = "";
                splitExpression.Numbers[i] = "";
                splitExpression.Numbers[i + 1] = calculatingNumbers;
            }

            return splitExpression.Numbers.Where(x => x != "").First().ToString();
        }
    }
}