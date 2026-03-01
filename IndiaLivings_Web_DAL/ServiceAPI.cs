using IndiaLivings_Web_DAL.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace IndiaLivings_Web_DAL
{
    public class ServiceAPI
    {
        // Make configuration and url fields static so static methods can access them
        private readonly IConfiguration _configuration;
        private static string baseurl = string.Empty;

        public ServiceAPI(IConfiguration configuration)
        {
            _configuration = configuration;
            baseurl = _configuration?["ApiUrl"] ?? string.Empty;
        }

        public static void Initialize(IConfiguration configuration)
        {
            baseurl = configuration["ApiUrl"] ?? string.Empty;
        }

        public static string Get_async_Api(string url)
        {
            string fullurl = baseurl + url;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            string responseBody = string.Empty;
            using (HttpClient client = new HttpClient(handler))
            {
                try
                {
                    // Make the request headers consistent
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    var response = client.GetAsync(fullurl).Result;

                    // Always read the response body so firewall messages can be inspected
                    responseBody = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseBody}", null, "ServiceAPI.Get_async_Api");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                }

                return responseBody;
            }
        }

        public static async Task<string> GetAsyncApi(string url)
        {
            string fullurl = baseurl + url;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            string responseBody = string.Empty;
            using (HttpClient client = new HttpClient(handler))
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    var response = await client.GetAsync(fullurl);

                    responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseBody}", null, "ServiceAPI.GetAsyncApi");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                }

                return responseBody;
            }
        }

        public static async Task<string> PostApiAsync(string apiUrl, object clsObject = null)
        {
            string fullurl = baseurl + apiUrl;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (message, cert, chain, sslPolicyErrors) => true
            };

            try
            {
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    client.Timeout = TimeSpan.FromSeconds(30);

                    string jsonPostData = JsonConvert.SerializeObject(clsObject);
                    using var content = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(fullurl, content);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return responseBody;

                    ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseBody}", null, "ServiceAPI.PostApiAsync");
                    return $"Error: {response.StatusCode}, {responseBody}";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return string.Empty;
            }
        }

        public static string Post_Api(string apiUrl, object clsObject = null)
        {
            string fullurl = baseurl + apiUrl;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
            var responseString = string.Empty;

            using (var client = new HttpClient(handler))
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    client.Timeout = TimeSpan.FromSeconds(30);
                    string jsonPostData = JsonConvert.SerializeObject(clsObject);
                    var content = new StringContent(jsonPostData, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(fullurl, content).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseString}", null, "ServiceAPI.Post_Api");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                }
            }

            return responseString;
        }

        public static async Task<string> PostMultipartApi(string apiUrl, MultipartFormDataContent form)
        {
            string fullurl = baseurl + apiUrl;
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                var response = await client.PostAsync(fullurl, form);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseString}", null, "ServiceAPI.PostMultipartApi");
                }

                return response.IsSuccessStatusCode ? responseString : $"Error: {response.StatusCode}, {responseString}";
            }
            catch (Exception ex)
            {
                ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                return "Exception occurred.";
            }
        }

        public static string Put_Api(string apiUrl, object clsObject = null)
        {
            string fullurl = baseurl + apiUrl;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
            var responseString = string.Empty;
            using (var client = new HttpClient(handler))
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    client.Timeout = TimeSpan.FromSeconds(30);
                    string jsonPutData = JsonConvert.SerializeObject(clsObject);
                    var content = new StringContent(jsonPutData, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(fullurl, content).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseString}", null, "ServiceAPI.Put_Api");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                }
            }
            return responseString;
        }

        public static string Delete_Api(string apiUrl)
        {
            string fullurl = baseurl + apiUrl;
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
            var responseString = string.Empty;
            using (var client = new HttpClient(handler))
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("IndiaLivingsClient/1.0");

                    client.Timeout = TimeSpan.FromSeconds(30);
                    var response = client.DeleteAsync(fullurl).Result;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        ErrorLog.insertErrorLog($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {responseString}", null, "ServiceAPI.Delete_Api");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                }
            }
            return responseString;
        }
    }
}
