using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using TradingApplication.API;

namespace TradingApplication.Model
{
    public static class GDAXAPIClient
    {
        static string config_API_Passphrase;
        static string config_API_Key;
        static string config_API_Secret;
        static string UserAgent;

        static GDAXAPIClient()
        {
            XElement Xdoc = XElement.Load("APIData.xml");
            IEnumerable<XElement> childList =
            from el in Xdoc.Elements()
            select el;
            foreach (XElement e in childList)
            {
                
                if (e.Name.LocalName == "config_API_Passphrase")
                    config_API_Passphrase = e.Value;
                if (e.Name.LocalName == "config_API_Key")
                    config_API_Key = e.Value;
                if (e.Name.LocalName == "config_API_Secret")
                    config_API_Secret = e.Value;
                if (e.Name.LocalName == "UserAgent")
                    UserAgent = e.Value;
            }
        }

        public static async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            string ts = GetNonce();
            string method = "/products";
            string str_GDAX_Main = "https://api.gdax.com";
            string sig = GetSignature(ts, "GET", method, string.Empty);
            ObservableCollection<Product> myproducts;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(str_GDAX_Main);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", config_API_Key);
                client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", sig);
                client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", ts);
                client.DefaultRequestHeaders.Add("CB-ACCESS-PASSPHRASE", config_API_Passphrase);
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                HttpResponseMessage response = client.GetAsync(method).Result;
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                myproducts = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
            }
            return myproducts;
        }

        public static async Task<ProdTicker> GetProdTickerAsync(string id)
        {
            string ts = GetNonce();
            string method = "/products/" + id + "/ticker";
            string str_GDAX_Main = "https://api.gdax.com";
            string sig = GetSignature(ts, "GET", method, string.Empty);
            ProdTicker myprodticker = new ProdTicker();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(str_GDAX_Main);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", config_API_Key);
                client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", sig);
                client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", ts);
                client.DefaultRequestHeaders.Add("CB-ACCESS-PASSPHRASE", config_API_Passphrase);
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                HttpResponseMessage response = client.GetAsync(method).Result;
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                myprodticker = JsonConvert.DeserializeObject<ProdTicker>(json);
            }
            return myprodticker;
        }

        public static async Task<ProdStats> GetProdStats(string id)
        {
            string ts = GetNonce();
            string method = "/products/" + id + "/stats";
            string str_GDAX_Main = "https://api.gdax.com";
            string sig = GetSignature(ts, "GET", method, string.Empty);
            ProdStats myprodstats = new ProdStats();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(str_GDAX_Main);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", config_API_Key);
                client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", sig);
                client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", ts);
                client.DefaultRequestHeaders.Add("CB-ACCESS-PASSPHRASE", config_API_Passphrase);
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                HttpResponseMessage response = client.GetAsync(method).Result;
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                myprodstats = JsonConvert.DeserializeObject<ProdStats>(json);
            }
            return await Task.Run(() => myprodstats);
        }

        public static async Task<Accounts> GetAccountsAsync()
        {
            string ts = GetNonce();
            string method = "/accounts";
            string str_GDAX_Main = "https://api.gdax.com";
            string sig = GetSignature(ts, "GET", method, string.Empty);
            Accounts myaccounts = new Accounts();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(str_GDAX_Main);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("CB-ACCESS-KEY", config_API_Key);
                client.DefaultRequestHeaders.Add("CB-ACCESS-SIGN", sig);
                client.DefaultRequestHeaders.Add("CB-ACCESS-TIMESTAMP", ts);
                client.DefaultRequestHeaders.Add("CB-ACCESS-PASSPHRASE", config_API_Passphrase);
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
                HttpResponseMessage response = client.GetAsync(method).Result;
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                myaccounts.account_list = JsonConvert.DeserializeObject<List<Account>>(json);
                
            }
            return myaccounts;
        }

        public static string GetNonce()
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
        }
        public static string GetSignature(string nonce, string method, string url, string body)
        {
            string message = string.Concat(nonce, method.ToUpper(), url, body);
            var encoding = new ASCIIEncoding();
            byte[] keyByte = Convert.FromBase64String(config_API_Secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

    }


}