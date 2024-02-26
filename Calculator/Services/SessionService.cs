using Microsoft.AspNetCore.Http;
using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void set(string expression, string result)
        {
            var expressionSession = _session.GetString("expression");
            var resultSession = _session.GetString("result");

            if (expressionSession == null)
            {
                _session.SetString("expression", expression);
                _session.SetString("result", result);
            }
            else
            {
                _session.SetString("expression", expressionSession + " " + expression);
                _session.SetString("result", resultSession + " " + result);
            }
        }

        public List<string> get(string param)
        {
            var sessionData = _session.GetString(param);

            if (sessionData != null)
            {
                return sessionData.Split(" ").ToList();
            }

            return new List<string> { };
        }
    }
}