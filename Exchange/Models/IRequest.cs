namespace Exchange.Models
{
    public interface IRequest
    {
        string CreateRequest(string uri, string date, string currencyFrom, string currencyTo);
    }
}
