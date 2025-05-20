using NewsApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApplication.Service
{
    public class ApiService
    {
        public async Task<Root> GetNews(string categoryName, string languageCode) 
        {
            //var httpClient = new HttpClient();
            //var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?category=general&apikey=2f8a4946c370cc36df5700cbdbabc4c9&lang=en&topic= +categoryName.ToLower()");
            //return JsonConvert.DeserializeObject<Root>(response);

            var httpClient = new HttpClient();
            string category = categoryName.ToLower();
            string lang = languageCode.ToLower();

            string url = $"https://gnews.io/api/v4/top-headlines?category={category}&lang={lang}&apikey=2f8a4946c370cc36df5700cbdbabc4c9";
            var response = await httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Root>(response);

        }

        //public async Task<Root> GetNewsByCountry(string countryCode)
        //{
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync("https://gnews.io/api/v4/top-headlines?category=general&apikey=2f8a4946c370cc36df5700cbdbabc4c9&lang=en&topic= +countryCode.ToLower()");
        //    return JsonConvert.DeserializeObject<Root>(response);

        //}
    }
}
