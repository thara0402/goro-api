using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Goro.Api.Infrastructure.Models;
using Newtonsoft.Json;

namespace Goro.Api.Infrastructure
{
    public static class GeocodeClient
    {
        public static async Task<Geocode> GetGeocodeAsync(string address)
        {
            var result = new Geocode();
            var requestUri = String.Format("http://maps.google.com/maps/api/geocode/json?address={0}&sensor=false", HttpUtility.UrlEncode(address));
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ja-JP"));
                var response = await client.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<Geocode>(response);
            }
            return result;
        }
    }
}