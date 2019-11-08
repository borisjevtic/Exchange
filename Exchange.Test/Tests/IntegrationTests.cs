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
            const string result = "0,156564317";

            var rates = new GetInfoFromApi();
            var rate = rates.GetExchangeRate(request);

            Assert.AreEqual(rate.ToString(), result);
        }

        [Test]
        public void CallExternApiFor20Dates()
        {
            //"https://api.exchangeratesapi.io/history?start_at=2018-02-01&end_at=2018-02-01&symbols=&base=SEK";

            const string requestPart1 = "https://api.exchangeratesapi.io/history?start_at=2018-02-";
            const string requestPart2 = "&end_at=2018-02-";
            const string requestPart3 = "&symbols=&base=SEK";

            const string max = "0,1573301069";
            const string min = "0,1555219517";
            const string average = "0,1565726749";

            var rates = new GetInfoFromApi();
            var allRates = new List<decimal>();

            for (var i = 0; i < 20; i++)
            {
                var rate = rates.GetExchangeRate(requestPart1 + 
                                                    i.ToString().PadLeft(2, '0') +
                                                    requestPart2 +
                                                    i.ToString().PadLeft(2, '0') +
                                                    requestPart3);

                if (rate != null)
                    allRates.Add(decimal.Parse(rate.ToString()));
            }
            
            Assert.AreEqual(allRates.Max().ToString(), max);
            Assert.AreEqual(allRates.Min().ToString(), min);
            Assert.AreEqual(Math.Round(allRates.Average(),10).ToString(), average);
        }
    }
}
