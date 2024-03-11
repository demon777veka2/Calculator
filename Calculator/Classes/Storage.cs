using Calculator.Classes.Interfaces;
using Calculator.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace Calculator.Classes
{
    public class Storage : IStorage
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public Storage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CalculatorSolution> GetCalculatorSolutions()
        {
            var calculatorSolutions = _session.Get<List<CalculatorSolution>>("calculatorSolutions");

            if (calculatorSolutions != null)
            {
                return calculatorSolutions;
            }

            return new List<CalculatorSolution>();
        }
        public void AddCalculatorSolution(string expression, string result)
        {
            var calculatorSolutions = _session.Get<List<CalculatorSolution>>("calculatorSolutions");

            if (calculatorSolutions == null)
            {
                calculatorSolutions = new List<CalculatorSolution>()
                {
                    new CalculatorSolution(expression, result)
                };

                _session.Set<List<CalculatorSolution>>("calculatorSolutions", calculatorSolutions);
            }
            else
            {
                calculatorSolutions.Add(new CalculatorSolution(expression, result));

                _session.Set<List<CalculatorSolution>>("calculatorSolutions", calculatorSolutions);
            }
        }
    }
}