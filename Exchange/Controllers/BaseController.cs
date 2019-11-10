using System;
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
        public string Get(string[] dates, string currencyFrom, string currencyTo)
        {
            GetInfoFromApi rates = new GetInfoFromApi();
            Dictionary<string, decimal> allRates = new Dictionary<string, decimal>();

            foreach (var date in dates)
            {
                var rate = rates.GetExchangeRate(_request.CreateRequest(uri, date, currencyFrom, currencyTo));
                if (rate != null)
                    allRates.Add(rate.Item1, rate.Item2);
            }

            var minimumValue = allRates.Min(x => x.Value);
            var minimumKey = allRates.FirstOrDefault(x => x.Value == (minimumValue)).Key;
            var maximumValue = allRates.Max(x => x.Value);
            var maximumKey = allRates.FirstOrDefault(x => x.Value == (maximumValue)).Key;
            var averageValue = Math.Round(allRates.Average(x => x.Value),12);

            string result = "A min rate of " + minimumValue + " on " + minimumKey +
                            "\nA max rate of " + maximumValue + " on " + maximumKey +
                            "\nAn average rate of " + averageValue;
            return result; 
        }
    }
}