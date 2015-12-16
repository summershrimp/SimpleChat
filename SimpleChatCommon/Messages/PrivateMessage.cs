using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon.Messages
{
    public class PrivateMessage:BaseMessage
    {
        public string FromNick { get; set; }
        public string ToNick { get; set; }
        public string Content { get; set; }

        PrivateMessage() : base("private"){}

        public PrivateMessage(string from, string to, string msg) : base("private")
        {
            FromNick = from;
            ToNick = to;
            Content = msg;
        }

    }
}
