using Calculator.Models;
using Calculator.Services;
using Calculator.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;
        private readonly ISessionService _sessionService;

        public CalculatorController (ICalculatorService calculatorService, ISessionService sessionService) 
        {
            _calculatorService = calculatorService;
            _sessionService = sessionService;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Calculate(string input)
        {
            bool isValidInput = _calculatorService.isValidationExpression(input);

            if (isValidInput)
            {
                int result = _calculatorService.Calculate(input);
                ViewData["result"] = result;
                _sessionService.set(input, result.ToString());
            }
            else
            {
                ViewData["error"] = "Не корректно введено выражение";
            }

            ViewData["expression"] = input;

            ViewBag.countExpressions = _sessionService.get("expression").Count();
            ViewBag.expressions = _sessionService.get("expression");
            ViewBag.results = _sessionService.get("result");

            return View("~/Views/Calculator/Index.cshtml");
        }
    }
}
