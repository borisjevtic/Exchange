using System;
using System.Collections.Generic;
using System.Linq;
using Exchange.Services;
using NUnit.Framework;

namespace Exchange.Test.Tests
{
    public class IntegrationTests
    {
        [Test]
        public void CallExternApiForOneDate()
        {
            const string request = "https://api.exchangeratesapi.io/history?start_at=2018-02-01&end_at=2018-02-01&symbols=&base=SEK";
            const decimal result = (decimal) 0.156564317;

            var rates = new GetInfoFromApi();
            var rate = rates.GetExchangeRate(request);

            Assert.AreEqual(rate.Item2, result);
        }

        [Test]
        public void CallExternApiFor20Dates()
        {
            //"https://api.exchangeratesapi.io/history?start_at=2018-02-01&end_at=2018-02-01&symbols=&base=SEK";

            const string requestPart1 = "https://api.exchangeratesapi.io/history?start_at=2018-02-";
            const string requestPart2 = "&end_at=2018-02-";
            const string requestPart3 = "&symbols=&base=SEK";

            const decimal max = (decimal) 0.1573301069;
            const decimal min = (decimal) 0.1555219517;
            const decimal average = (decimal) 0.1565726749;

            var rates = new GetInfoFromApi();
            Dictionary<string, decimal> allRates = new Dictionary<string, decimal>();

            for (var i = 0; i < 20; i++)
            {
                var rate = rates.GetExchangeRate(requestPart1 + 
                                                    i.ToString().PadLeft(2, '0') +
                                                    requestPart2 +
                                                    i.ToString().PadLeft(2, '0') +
                                                    requestPart3);

                if (rate != null)
                    allRates.Add(rate.Item1, rate.Item2);
            }

            var minimumValue = allRates.Min(x => x.Value);
            var maximumValue = allRates.Max(x => x.Value);
            var averageValue = allRates.Average(x => x.Value);

            Assert.AreEqual(minimumValue, min);
            Assert.AreEqual(maximumValue, max);
            Assert.AreEqual(Math.Round(averageValue, 10), average);
        }

        [Test]
        public void CallExternApiFor3Dates()
        {
        
        //"https://api.exchangeratesapi.io/history?start_at=2018-02-01&end_at=2018-02-01&symbols=&base=SEK";

            const string uri = "https://api.exchangeratesapi.io";

            string[] dates = {"2018-02-01", "2018-02-15", "2018-03-01"};
            var currencyFrom = "SEK";
            var currencyTo = "NOK";
            const decimal min = (decimal) 0.9546869595;
            const decimal max = (decimal) 0.9815486993;
            const decimal average = (decimal) 0.970839476467;

            Request request = new Request();
            GetInfoFromApi rates = new GetInfoFromApi();
            Dictionary<string, decimal> allRates = new Dictionary<string, decimal>();

            foreach (var date in dates)
            {
                var rate = rates.GetExchangeRate(request.CreateRequest(uri, date, currencyFrom, currencyTo));
                if (rate != null)
                    allRates.Add(rate.Item1, rate.Item2);
            }
            
            var minimumValue = allRates.Min(x => x.Value);
            var maximumValue = allRates.Max(x => x.Value);
            var averageValue = allRates.Average(x => x.Value);
            
            Assert.AreEqual(minimumValue, min);
            Assert.AreEqual(maximumValue, max);
            Assert.AreEqual(Math.Round(averageValue,12), average);
        }
    }
}
