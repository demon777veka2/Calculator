using Calculator.Classes.Interfaces;
using Calculator.Models;
using Calculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IStringCalculator _stringCalculator;
        private readonly IStorage _storage;

        public CalculatorController(IStringCalculator stringCalculator, IStorage storage)
        {
            _stringCalculator = stringCalculator;
            _storage = storage;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index(string expression = "")
        {
            CalculatorHistory calculatorHistory = _storage.getCalculatorHistory();
            CalculatorView calculatorView;
            string mathExpression;
            string result;


            if (calculatorHistory.Expressions.Count != 0)
            {
                mathExpression = calculatorHistory.Expressions[calculatorHistory.Expressions.Count - 1];
                result = calculatorHistory.Expressions[calculatorHistory.Expressions.Count - 1];

            }
            else
            {
                mathExpression = "";
                result = "";
            }

            if (expression != "")
            {
                calculatorView = new CalculatorView(
                    expression,
                    "",
                    calculatorHistory,
                    "Не корректно введено выражение");
            }
            else
            {
                calculatorView = new CalculatorView(
                   mathExpression,
                   result,
                   calculatorHistory,
                   null);
            }

            return View(calculatorView);
        }

        [HttpPost]
        [Route("/")]
        public IActionResult calculate(string input)
        {
            bool isCorrectExpression = _stringCalculator.isCorrectExpression(input);

            if (!isCorrectExpression)
            {
                return RedirectToAction("Index", "Calculator", new { expression = input });
            }

            string result = _stringCalculator.Calculate(input);
            _storage.addCalculatorHistory(input, result.ToString());

            CalculatorHistory calculatorHistory = _storage.getCalculatorHistory();
            CalculatorView calculatorView = new CalculatorView(
              calculatorHistory.Expressions[calculatorHistory.Expressions.Count - 1],
              calculatorHistory.Results[calculatorHistory.Results.Count - 1],
              calculatorHistory,
              null);

            return View("Index", calculatorView);
        }
    }
}
