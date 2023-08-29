using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hjc.ChatGPT
{
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }

        public Message(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}
