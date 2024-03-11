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
        public IActionResult Index()
        {
            List<CalculatorSolution> calculatorSolutions = _storage.GetCalculatorSolutions();
            CalculatorOutput calculatorView;
            string mathExpression;
            string result;

            if (calculatorSolutions.Any())
            {
                mathExpression = calculatorSolutions.Last().Expression;
                result = calculatorSolutions.Last().Result;
            }
            else
            {
                mathExpression = "";
                result = "";
            }

            calculatorView = new CalculatorOutput();
            {
                calculatorView.Expression = mathExpression;
                calculatorView.Result = result;
                calculatorView.CalculatorSolutions = calculatorSolutions;
            }

            return View(calculatorView);
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Сalculate(string input)
        {
            bool isCorrectExpression = _stringCalculator.IsCorrectExpression(input);

            if (!isCorrectExpression)
            {
                List<CalculatorSolution> calculatorSolutions = _storage.GetCalculatorSolutions();

                CalculatorOutput calculatorView = new CalculatorOutput();
                {
                    calculatorView.Expression = input;
                    calculatorView.CalculatorSolutions = calculatorSolutions;
                    calculatorView.Error = "Не корректно введено выражение";
                };

                return View("Index", calculatorView);
            }

            string result = _stringCalculator.Calculate(input);
            _storage.AddCalculatorSolution(input, result.ToString());

            return RedirectToAction("Index");
        }
    }
}