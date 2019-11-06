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
            this._request = request;
        }

        [HttpGet]
        public Result Get(string[] dates, string currencyFrom, string currencyTo)
        {

            GetInfoFromApi rates = new GetInfoFromApi();
            List<decimal> allRates = new List<decimal>();
            object rate = string.Empty;

            foreach (var date in dates)
            {
                rate = rates.GetExchangeRate(_request.CreateRequest(uri, date, currencyFrom, currencyTo));
                if (rate != null)
                    allRates.Add(decimal.Parse(rate.ToString()));
            }

            Result result = new Result();
            result.MinimumRate = allRates.Min();
            result.MaximumRate = allRates.Max();
            result.AverageRate = allRates.Average();

            return result;
        }
    }
}