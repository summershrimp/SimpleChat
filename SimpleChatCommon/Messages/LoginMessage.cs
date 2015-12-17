using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatCommon.Messages
{
    public class LoginMessage : BaseMessage
    {
        public string Nickname{ get; set; }
        LoginMessage() : base("login") { }
        public LoginMessage(string nick) : base("login")
        {
            Nickname = nick;
        }
    }
}
