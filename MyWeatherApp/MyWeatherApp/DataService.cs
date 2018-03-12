using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWeatherApp
{

    // helper class to make async requests against api
    public class DataService
    {
        public static async Task<dynamic>RetrieveDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            
            string json = null;
            if (response != null)
            {
                json = response.Content.ReadAsStringAsync().Result;
            }

            return json;
        }
    }
}
