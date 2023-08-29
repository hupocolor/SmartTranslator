// See https://aka.ms/new-console-template for more information
using Azure.Core;
using Hjc.ChatGPT;
using Newtonsoft.Json;

string token = "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";
MyRequestData requestData = new MyRequestData();
requestData.messages = new List<Message>
{
    new Message("user",Prompt.Word+"手机")
};
string res = await ChatGPTConnect.GetResponseAsync(requestData, token);
Console.WriteLine(res);