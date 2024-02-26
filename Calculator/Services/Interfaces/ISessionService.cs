namespace Calculator.Services.Interfaces
{
    public interface ISessionService
    {
        public void set(string expression, string result);
        public List<string> get(string param);
    }
}
