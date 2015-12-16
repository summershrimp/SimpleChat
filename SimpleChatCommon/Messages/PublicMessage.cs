using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon.Messages
{
    public class PublicMessage:BaseMessage
    {
        public String FromNick { get; set; }
        public String Content { get; set; }
        PublicMessage() : base("public") { }
        public PublicMessage(string from, string msg) : base("public")
        {
            FromNick = from;
            Content = msg;
        }
    }
}
