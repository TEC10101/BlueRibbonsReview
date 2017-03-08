using Blue_Ribbon.Models;
using Blue_Ribbon.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Blue_Ribbon.BLL
{
    public partial class API
    {
        // Attempts to populate the necessary fields of the DealValidate1ViewModel to create a new deal in the database
        public static void Bestbuy(DealValidate1ViewModel vm)
        {
            //Affiliate Information
            string bestbuyApiKey = "REMOVED"; // BestBuy Product Advertising API
            string linkshareAffiliateKey = "REMOVED"; // Affiliate key

            //URL to make the API call
            string URL = String.Format(
                "https://api.bestbuy.com/v1/products" +
                $"/{vm.ASIN}.xml" + // Best Buy calls this <SKU>.xml
                $"?apiKey={bestbuyApiKey}") +
                $"&LID={linkshareAffiliateKey}"; // More Info: https://developer.bestbuy.com/affiliate-program
              

            //Create XML document
            XmlDocument bestbuyProductXML = new XmlDocument();

            //Get response and create XML tree
            Stream bestbuyXMLStream = WebRequest.Create(URL).GetResponse().GetResponseStream();
            bestbuyProductXML.Load(new StreamReader(bestbuyXMLStream));
            XmlNode xml = bestbuyProductXML.DocumentElement;

            //Get NodeList of each part of the API we use
            XmlNodeList name = xml.SelectNodes("//name");
            XmlNodeList originalPrice = xml.SelectNodes("//regularPrice");
            XmlNodeList retailPrice = xml.SelectNodes("//salePrice");
            XmlNodeList imageUrl = xml.SelectNodes("//largeImage");
            XmlNodeList productUrl = xml.SelectNodes("//linkShareAffiliateUrl");
            XmlNodeList marketplace = xml.SelectNodes("//marketplace");
            XmlNodeList description = xml.SelectNodes("//longDescription");

            if (name.Count != 0)
            {
                vm.Name = name[0].InnerText;
            }
            if (retailPrice.Count != 0)
            {
                //RetailPrice is the BlueRibbon price
                vm.RetailPrice = retailPrice[0].InnerText;
            }
            if (originalPrice.Count != 0)
            {
                vm.OriginalPrice = originalPrice[0].InnerText;
            }
            if (productUrl.Count != 0)
            {
                vm.VendorsPurchaseURL = productUrl[0].InnerText;
            }
            if (imageUrl.Count != 0)
            {
                vm.ImageUrl = imageUrl[0].InnerText;
            }
            if (description.Count != 0)
            {
                vm.Description = Regex.Replace(WebUtility.HtmlDecode(description[0].InnerText), "&[^>]*;", "");
            }
        }


        // Update price of products currently in dB
        public static async Task<string> BestbuyPriceCheck(Deal deal, List<ExchangeRates> ER)
        {
            // AffiliateInformation
            string bestbuyApiKey = "REMOVED"; // BestBuy Product Advertising API
            string linkshareAffiliateKey = "REMOVED";

            // Limit to 5 API calls per second
            Task.Delay(200).Wait();

            //URL to make the API call
            string URL = String.Format(
                "https://api.bestbuy.com/v1/products/" +
                $"{deal.ASIN}.xml" +
                $"?apiKey={bestbuyApiKey}") +
                $"&LID={linkshareAffiliateKey}";

            //Create XML document
            XmlDocument bestbuyProductXML = new XmlDocument();
            Stream bestbuyXMLStream = WebRequest.Create(URL).GetResponse().GetResponseStream();
            bestbuyProductXML.Load(new StreamReader(bestbuyXMLStream));
            XmlNode xml = bestbuyProductXML.DocumentElement;

            // Pull the current price:
            XmlNodeList retailPrice = xml.SelectNodes("//salePrice");

            // return string for price
            return retailPrice[0].InnerText;
        }
    }

    //bestbuy's api requires special language and tagging
    public partial class DealProcessing
    {
        public static void Bestbuy(ProductSortViewModel vm)
        {
            vm.requiredHTML = "<br /><a href=\"https://developer.bestbuy.com\"><img src=\"https://developer.bestbuy.com/images/fulfilled-through-bestbuy.png\" alt=\"Best Buy Developer\"></a>";
        }
    }
}