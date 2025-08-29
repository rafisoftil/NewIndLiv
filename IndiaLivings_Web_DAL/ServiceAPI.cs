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

namespace IndiaLivings_Web_DAL
{
    public class ServiceAPI
    {
        public static string Get_async_Api(string url)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };
            string responseBody=string.Empty;
            using (HttpClient client = new HttpClient(handler))
            {
                HttpResponseMessage response = new HttpResponseMessage();// await client.GetAsync(url);
                try
                {
                    // Make the GET request asynchronously and return the response as a string.
                    response= client.GetAsync(url).Result;
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

        public static string Post_Api(string apiUrl, object clsObject = null)
        {
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
                    var response = client.PostAsync(apiUrl, content).Result;
                    if(response.IsSuccessStatusCode)
                        responseString= response.Content.ReadAsStringAsync().Result;
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
            using var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            try
            {
                var response = await client.PostAsync(apiUrl, form);
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
                    var response = client.PutAsync(apiUrl, content).Result;
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
                    var response = client.DeleteAsync(apiUrl).Result;
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
