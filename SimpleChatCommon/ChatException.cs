using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon
{
    public class ChatException:Exception
    {
        public ChatException(string e) : base(e)
        { }
    }
}
