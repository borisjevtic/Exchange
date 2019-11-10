using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Exchange.Services
{
    public class GetInfoFromApi
    {
        public Tuple<string, decimal> GetExchangeRate(string request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(request);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync().Result;
                        ExpandoObject expandedJson = JsonConvert.DeserializeObject<ExpandoObject>(jsonString);
                        var rates = (IDictionary<string, object>)expandedJson.FirstOrDefault(x => x.Key == "rates").Value;
                        foreach (var rate in rates)
                        {
                            foreach (var exchangeDate in (IDictionary<string, object>)rate.Value)
                            {
                                var result = Tuple.Create(rate.Key, decimal.Parse(exchangeDate.Value.ToString()));
                                return result;
                            }
                        }
                    }

                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}