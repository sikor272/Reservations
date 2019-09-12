using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiControl
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        public static string Token { get; set; }
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            if(Token != null)
                ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
