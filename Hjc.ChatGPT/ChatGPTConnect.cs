using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.ChatGPT
{
    /// <summary>
    /// ChatGPT连接
    /// </summary>
    public class ChatGPTConnect
    {
        public static async Task<string> GetResponseAsync(MyRequestData request,string token)
        {
            // Your API key from OpenAI
            string apiKey = token;

            // The endpoint URL for the ChatGPT API
            string apiUrl = "https://api.openai.com/v1/chat/completions";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Serialize the request data to JSON and create StringContent with the appropriate Content-Type
                var jsonRequestData = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
                // Send the API request with the httpContent
                var response = await client.PostAsync(apiUrl, httpContent);
                //await Console.Out.WriteLineAsync(jsonRequestData);
                //await Console.Out.WriteLineAsync(response.ToString());

                // Handle the API response
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
            }
            Debug.WriteLine("连接chatGPT异常");
            throw new Exception("token过期或连接失败");
        }
    }
}
