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
                HttpResponseMessage response = new HttpResponseMessage();// await client.GetAsync(url);
                try
                {
                    // Make the GET request asynchronously and return the response as a string.
                    response = client.GetAsync(fullurl).Result;
                    response.EnsureSuccessStatusCode(); // Throws an exception for HTTP error codes.

                    // Read the response content as a string.
                    responseBody = response.Content.ReadAsStringAsync().Result;
                    //res=JsonConvert.DeserializeObject(responseBody);
                    //return response;
                }
                catch (Exception ex)
                {
                    // Log error or handle the exception appropriately.
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                    //return string.Empty; // Return an empty string in case of failure.
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
                HttpResponseMessage response = new HttpResponseMessage();// await client.GetAsync(url);
                try
                {
                    // Make the GET request asynchronously and return the response as a string.
                    response = await client.GetAsync(fullurl);
                    response.EnsureSuccessStatusCode(); // Throws an exception for HTTP error codes.

                    // Read the response content as a string.
                    responseBody = await response.Content.ReadAsStringAsync();
                    //res=JsonConvert.DeserializeObject(responseBody);
                    //return response;
                }
                catch (Exception ex)
                {
                    // Log error or handle the exception appropriately.
                    ErrorLog.insertErrorLog(ex.Message, ex.StackTrace, ex.Source);
                    //return string.Empty; // Return an empty string in case of failure.
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
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.Timeout = TimeSpan.FromSeconds(30);

                    string jsonPostData = JsonConvert.SerializeObject(clsObject);
                    using var content = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(fullurl, content);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return responseBody;

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

            // Use a static HttpClient for better performance (reuse the instance)
            using (var client = new HttpClient(handler))
            {
                try
                {
                    // Set the default headers for JSON content
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30); // Optional: Timeout after 30 seconds (adjust as needed)
                    string jsonPostData = JsonConvert.SerializeObject(clsObject);
                    var content = new StringContent(jsonPostData, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(fullurl, content).Result;
                    if (response.IsSuccessStatusCode)
                        responseString = response.Content.ReadAsStringAsync().Result;
                    else
                        responseString = $"Error: {response.StatusCode}, {response.Content.ReadAsStringAsync().Result}";

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
                var response = await client.PostAsync(fullurl, form);
                var responseString = await response.Content.ReadAsStringAsync();

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
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);
                    string jsonPutData = JsonConvert.SerializeObject(clsObject);
                    var content = new StringContent(jsonPutData, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(fullurl, content).Result;
                    if (response.IsSuccessStatusCode)
                        responseString = response.Content.ReadAsStringAsync().Result;
                    else
                        responseString = $"Error: {response.StatusCode}, {response.Content.ReadAsStringAsync().Result}";
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
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var response = client.DeleteAsync(fullurl).Result;
                    if (response.IsSuccessStatusCode)
                        responseString = response.Content.ReadAsStringAsync().Result;
                    else
                        responseString = $"Error: {response.StatusCode}, {response.Content.ReadAsStringAsync().Result}";
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
