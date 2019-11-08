using System.Collections.Generic;
using System.Linq;
using Exchange.Models;
using Exchange.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Controllers
{
    [Route("api")]
    public class BaseController : Controller
    {
        private const string uri = "https://api.exchangeratesapi.io";

        private readonly IRequest _request;
        public BaseController(IRequest request)
        {
            _request = request;
        }

        [HttpGet]
        public Result Get(string[] dates, string currencyFrom, string currencyTo)
        {

            GetInfoFromApi rates = new GetInfoFromApi();
            List<decimal> allRates = new List<decimal>();

            foreach (var date in dates)
            {
                var rate = rates.GetExchangeRate(_request.CreateRequest(uri, date, currencyFrom, currencyTo));
                if (rate != null)
                    allRates.Add(decimal.Parse(rate.ToString()));
            }

            var result = new Result
            {
                MinimumRate = allRates.Min(), MaximumRate = allRates.Max(), AverageRate = allRates.Average()
            };

            return result;
        }
    }
}