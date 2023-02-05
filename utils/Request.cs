using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;
using TLEMAITRE1_nasa.models;
using System.Security.Policy;
using System.Windows;
using System.Windows.Media.Animation;

namespace TLEMAITRE1_nasa.utils
{
    public class Request
    {
        #region attribut
        private HttpClient _client;

        #endregion


        #region Construtor

        public Request(string uri)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion


        #region get
        public HttpClient GetClient() => _client;
        #endregion

        #region set
        public void SetClient(HttpClient client) => _client = client;
        #endregion


        #region method

        //make a get request to the api
        public async Task<T?> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }

        // make a get request with full path
        public static async Task<T?> GetAsyncFullPath<T>(string url)
        {
            HttpClient client = new HttpClient();
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }

        #endregion



    }
}
