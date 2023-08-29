using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.ChatGPT.Utils
{
    internal class JsonHelper
    {
        internal static async Task<string> GetJSONAsync(string input, string token, string prompt)
        {
            MyRequestData requestData = new MyRequestData();
            requestData.messages = new List<Message>
            {
                new Message("user",prompt),
                new Message("user",input)
            };
            return await ChatGPTConnect.GetResponseAsync(requestData, token);
        }
    }
}
