using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using SimpleChatCommon.Messages;
namespace SimpleChatServer
{
    static class Program
    {
        static Socket serverSocket;
        static Hashtable userList = Hashtable.Synchronized(new Hashtable());
  
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            IPAddress ip = IPAddress.Parse("0.0.0.0");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, 8500));
            serverSocket.Listen(10);
            Thread acceptThread = new Thread(AcceptClient);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ServerStatus());
        }

        private static void AcceptClient()
        {
            while (true)
            {
                Socket clientsock = serverSocket.Accept();
                ClientInfo client = new ClientInfo("",clientsock);
                Thread clientThread = new Thread(Worker);
                clientThread.Start(client);
            }
        }

        private static void Worker(object clientObj)
        {
            byte[] buffer = new byte[4096];
            byte[] sendbuffer;
            ClientInfo client = (ClientInfo)clientObj;
            bool online = false;
            while (client.Nickname == null || client.Nickname.Length == 0)
            {
                client.Client.Receive(buffer);
                try
                {
                    LoginMessage msg = (LoginMessage)JsonConvert.DeserializeObject(System.Text.ASCIIEncoding.UTF8.GetString(buffer));
                    if (msg.MsgType.Equals("login") || msg.Nickname != null || msg.Nickname.Length == 0 || msg.Nickname.ToLower().Equals("system") || userList.ContainsKey(msg.Nickname))
                        throw new Exception("no nickname");
                    client.Nickname = msg.Nickname;
                    userList.Add(client.Nickname, client);
                    PrivateMessage emsg = new PrivateMessage("System", "client.Nickname", "Hello!");
                    string str = JsonConvert.SerializeObject(emsg).ToString();
                    sendbuffer = System.Text.Encoding.UTF8.GetBytes(str);
                    client.Client.Send(sendbuffer);
                    online = true;
                }
                catch (SocketException)
                {
                    online = false;
                }
                catch (Exception e)
                {
                    doErrorMessage(client, e.Message);
                }
            }
            while (online && client.Client.Connected)
            {
                try
                {
                    BaseMessage msg = (BaseMessage)JsonConvert.DeserializeObject(System.Text.ASCIIEncoding.UTF8.GetString(buffer));
                    switch (msg.MsgType)
                    {
                        case "public": doPublicMessage((PublicMessage)msg); break;
                        case "private": doPrivateMessage((PrivateMessage)msg); break;
                        default: throw new Exception("no such message");

                    }
                }
                catch (SocketException)
                {
                    online = false;
                }
                catch (Exception e)
                {
                    doErrorMessage(client, e.Message);
                }
            }
        }

        private static void doPublicMessage(PublicMessage msg)
        {
            byte[] sendbuffer;
            string str = JsonConvert.SerializeObject(msg).ToString();
            sendbuffer = System.Text.Encoding.UTF8.GetBytes(str);
            foreach (DictionaryEntry de in userList)
            {
                if (de.Key.Equals(msg.FromNick))
                {
                    continue;
                }
                ((ClientInfo)de.Value).Client.Send(sendbuffer);
            }
        }

        private static void doPrivateMessage(PrivateMessage msg)
        {
            byte[] sendbuffer;
            string str = JsonConvert.SerializeObject(msg).ToString();
            sendbuffer = System.Text.Encoding.UTF8.GetBytes(str);
            if (userList.Contains(msg.ToNick))
            {
                ClientInfo de = (ClientInfo)userList[msg.ToNick];
                ((ClientInfo)de).Client.Send(sendbuffer);
            }
            else
            {
                throw new Exception("no such user");
            }
        }
        private static void doErrorMessage(ClientInfo client, string msg)
        {
            byte[] sendbuffer;
            ErrorMessage emsg = new ErrorMessage(msg);
            string str = JsonConvert.SerializeObject(emsg).ToString();
            sendbuffer = System.Text.Encoding.UTF8.GetBytes(str);
            client.Client.Send(sendbuffer);
        }
    }
}
