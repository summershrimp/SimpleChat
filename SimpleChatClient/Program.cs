using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using SimpleChatCommon.Messages;
using SimpleChatCommon;
namespace SimpleChatClient
{
    static class Program
    {
        static Socket clientSocket;
        static string ipAddr = "127.0.0.1";
        static string Nickname = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
			clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void Connect(string ip)
        {
            ipAddr = ip;
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), 8500));
        }
        public static void setNickname(string nickname)
        {
            Nickname = nickname;
        }
        public static void SendPublic(string msg)
        {
            PublicMessage pmsg = new PublicMessage(Nickname, msg);
            string str = JsonConvert.SerializeObject(pmsg).ToString();
            Common.doSend(clientSocket, str);
        }
        public static void SendPrivate(string toUser, string msg)
        {
            PrivateMessage pmsg = new PrivateMessage(Nickname, toUser, msg);
            string str = JsonConvert.SerializeObject(pmsg).ToString();
            Common.doSend(clientSocket, str);
        }
        public static void SendLogin()
        {
            LoginMessage pmsg = new LoginMessage(Nickname);
            string str = JsonConvert.SerializeObject(pmsg).ToString();
            Common.doSend(clientSocket, str);
        }

        public static BaseMessage Receive()
        {
            JObject json;
            BaseMessage msg = null;
            try
            {
                string str = Common.doReceive(clientSocket);
                json = (JObject)JsonConvert.DeserializeObject(str);
                switch ((string)json.GetValue("MsgType"))
                {
                    case "error":
                        msg = (ErrorMessage)JsonConvert.DeserializeObject<ErrorMessage>(str);
                        break;
                    case "public":
                        msg = (PublicMessage)JsonConvert.DeserializeObject<PublicMessage>(str);
                        break;
                    case "private":
                        msg = (PrivateMessage)JsonConvert.DeserializeObject<PrivateMessage>(str);
                        break;
                }
            }
            catch (SocketException)
            {
                throw new Exception("Server went down");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            return msg;
        }

    }
}
