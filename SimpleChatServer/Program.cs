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
using SimpleChatCommon;
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
            acceptThread.Start();
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
            ClientInfo client = (ClientInfo)clientObj;
            bool online = false;
            while (client.Nickname == null || client.Nickname.Length == 0)
            {
                try
                {
                    string str = Common.doReceive(client.Client);
                    LoginMessage msg = JsonConvert.DeserializeObject<LoginMessage>(str);
                    if (!msg.MsgType.Equals("login") || msg.Nickname == null || msg.Nickname.Length == 0 || msg.Nickname.ToLower().Equals("system") || userList.ContainsKey(msg.Nickname))
                        throw new Exception("no nickname");
                    client.Nickname = msg.Nickname;
                    userList.Add(client.Nickname, client);
                    PublicMessage emsg = new PublicMessage("System", "Welcome " + msg.Nickname);
                    doPublicMessage(emsg);
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
                    string t = Common.doReceive(client.Client);
                    BaseMessage msg = (BaseMessage)JsonConvert.DeserializeObject(t);
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
            string str = JsonConvert.SerializeObject(msg).ToString();
            foreach (DictionaryEntry de in userList)
            {
                if (de.Key.Equals(msg.FromNick))
                {
                    continue;
                }
                try
                {
                    Common.doSend(((ClientInfo)de.Value).Client, str);
                }
                catch(SocketException)
                {
                    //ignore
                }
            }
        }

        private static void doPrivateMessage(PrivateMessage msg)
        {
            string str = JsonConvert.SerializeObject(msg).ToString();
            
            if (userList.Contains(msg.ToNick))
            {
                ClientInfo de = (ClientInfo)userList[msg.ToNick];
                Common.doSend(de.Client, str);
            }
            else
            {
                throw new Exception("no such user");
            }
        }
        private static void doErrorMessage(ClientInfo client, string msg)
        {
            ErrorMessage emsg = new ErrorMessage(msg);
            string str = JsonConvert.SerializeObject(emsg).ToString();
           
            Common.doSend(client.Client, str);
        }

    }
}
