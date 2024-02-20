using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService : ICalculatorHttpService
    {
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetPath());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("api/Calculator/postalcodes");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }
        private string GetPath()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            string localpath = "";
            if (Directory.GetCurrentDirectory().IndexOf("bin") > 0)
            {
                localpath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("bin"));
            }
            else {
                localpath = Directory.GetCurrentDirectory();
            }
            
            builder.AddJsonFile(Path.Combine(localpath, "appsettings.json"));
            var root = builder.Build();
            var path = root.GetSection("CalculatorSettings").GetValue<string>("ApiUrl");
            if (path == null)
            {
                return "";
            }
            else {
                return path;
            }            
        }
        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetPath());            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("api/Calculator/history");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? [];
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetPath());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync("api/Calculator/calculate-tax", calculationRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               var result=  response.Content.ReadFromJsonAsync<CalculateResult>().Result;
                if (result != null)
                {
                    return result;
                }
                else
                    return new CalculateResult();
                //return Newtonsoft.Json.JsonConvert.DeserializeObject<CalculateResult>(response.Content.ToString());
            }
            else { 
            return new CalculateResult();
            }
        }
    }
}