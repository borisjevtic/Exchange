using Exchange.Models;

namespace Exchange.Services
{
    public class Request : IRequest
    {
        public string CreateRequest(string uri, string date, string currencyFrom, string currencyTo)
        {
            return uri + "/history?start_at=" + date + "&end_at=" + date + "&symbols=" + currencyTo + "&base=" + currencyFrom;
        }
    }
}
