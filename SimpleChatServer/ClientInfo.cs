using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace SimpleChatServer
{
    class ClientInfo
    {
        public string Nickname { get; set; }
        public Socket Client { get; set; }

        public ClientInfo(string nick, Socket sock)
        {
            Nickname = nick;
            Client = sock;
        }
    }
}
