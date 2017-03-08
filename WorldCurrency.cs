using Blue_Ribbon.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Blue_Ribbon.BLL
{
    public class WorldCurrency
    {
        private static BRContext db = new BRContext();

        public static double CurrencyToUSD(string currencyType)
        {
            //Api developer key
            string apiKey = "REMOVED";

            //Currencylayer api request
            string url = String.Format(
              "http://apilayer.net/api/live" +
              $"?access_key={apiKey}" +
              $"&currencies={currencyType}" +
              "&source=USD" +
              "&format=1");

            //Create instance of json file and turn it into a dictionary
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject jsonReadable = JObject.Parse(rawJson);
            IDictionary<string, JToken> quotes = (JObject)jsonReadable["quotes"];
            Dictionary<string, double> rates =
              quotes.ToDictionary(a => a.Key,
                                  a => (double)a.Value);

            //Get value out of dictionary
            double rate = rates.Last().Value;

            return rate;
        }
        public static async Task UpdateCurrencyExchangeRates()
        {
            bool continueUpdates = true;
            var exchangeRates = db.ExchangeRates.ToList();
            for (int i = 0; i < exchangeRates.Count; i++)
            {
                if (continueUpdates)
                {
                    try
                    {
                        exchangeRates[i].ExchangeRate = CurrencyToUSD(exchangeRates[i].CurrencyCode);
                    }
                    catch
                    {
                        continueUpdates = false;
                    }
                }
            }
            db.SaveChanges();
        }
    }
}