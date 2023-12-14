using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISTSU0_ADT_2023241.Client
{
    public class RestService
    {
        HttpClient client;

        public RestService(string baseurl)
        {
            Init(baseurl);
        }

        private void Init(string baseurl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }

        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            return items;
        }

        public T GetSingle<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public T Get<T>(string id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public TResult Post<TResult,TDto>(TDto item, string endpoint)
        {
            TResult result = default(TResult);
            HttpResponseMessage response =
                client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
            }
            return result;
        }

        public void Delete(string id, string endpoint)
        {
            HttpResponseMessage response =
                client.DeleteAsync(endpoint + "/" + id.ToString()).GetAwaiter().GetResult();

            response.EnsureSuccessStatusCode();
        }

        public TResult Put<TResult,TDto>(TDto item, string endpoint)
        {
            TResult result = default(TResult);
            HttpResponseMessage response =
                client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
            }
            return result;
        }
    }
}
