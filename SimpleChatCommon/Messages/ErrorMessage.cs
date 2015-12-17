using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon.Messages
{
    public class ErrorMessage:BaseMessage
    {
        public string ErrorStr { get; set; }
        ErrorMessage() : base("error"){ }
        public ErrorMessage(string msg):base("error")
        {
            ErrorStr = msg;
        }
    }
}
