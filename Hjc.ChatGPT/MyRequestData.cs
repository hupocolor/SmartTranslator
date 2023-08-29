using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.ChatGPT
{
    public class MyRequestData
    {
        public string model { get; set; } = "gpt-3.5-turbo";
        public List<Message> messages { get; set; }
    }
}
