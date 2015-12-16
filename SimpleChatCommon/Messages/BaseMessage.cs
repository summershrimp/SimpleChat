using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon.Messages
{
    public class BaseMessage
    {
        public string MsgType { get; set; }

        public BaseMessage(string msgType)
        {
            MsgType = msgType;
        }
    }
}
