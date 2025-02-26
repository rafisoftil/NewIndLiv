using IndiaLivings_Web_DAL.Models;
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

        public static async Task<string> Post_Api(string apiUrl)
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

                    // Send the GET request
                    var response = await client.GetAsync(apiUrl);

                    // Check the response status
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();  // Successful response
                    }
                    else
                    {
                        // Handle error response
                        responseString = $"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}";
                    }
                }
                catch (Exception ex)
                {
                    // Capture the exception and include more detailed information
                    responseString = $"Exception: {ex.Message}\nStackTrace: {ex.StackTrace}";
                    // Optionally log the exception here
                }
            }

            return responseString;
        }


    }
}
