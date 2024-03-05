using Calculator.Classes.Interfaces;
using Calculator.Models;
using Microsoft.AspNetCore.Http;
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

        public CalculatorHistory getCalculatorHistory()
        {
            var calculatorHistory = SessionExtensions.Get<CalculatorHistory>(_session, "calculatorHistory");

            if (calculatorHistory != null)
            {
                return calculatorHistory;
            }

            return new CalculatorHistory(new List<string>(), new List<string>());
        }
        public void addCalculatorHistory(string expression, string result)
        {
            var calculatorHistory = SessionExtensions.Get<CalculatorHistory>(_session, "calculatorHistory");

            if (calculatorHistory == null)
            {
                SessionExtensions.Set<CalculatorHistory>(_session, "calculatorHistory",
                  new CalculatorHistory(new List<string>() { expression }, new List<string>() { result }));
            }
            else
            {
                calculatorHistory.Expressions.Add(expression);
                calculatorHistory.Results.Add(result);

                SessionExtensions.Set<CalculatorHistory>(_session, "calculatorHistory", calculatorHistory);
            }
        }
        }
    }