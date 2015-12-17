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
using Newtonsoft.Json.Linq;
using SimpleChatCommon.Messages;
using SimpleChatCommon;
namespace SimpleChatServer
{
    static class Program
    {
        static Socket serverSocket;
        static Hashtable userList = Hashtable.Synchronized(new Hashtable());
        static ServerStatus serverStatusForm;
        static Thread acceptThread;
        public delegate void AnnounceHandle(string name);
        public static event AnnounceHandle AnnounceOnline;
        public static event AnnounceHandle AnnounceOffline;
        static List<Thread> threadList;
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
            acceptThread = new Thread(AcceptClient);
            acceptThread.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(serverStatusForm = new ServerStatus());
        }

        public static void CloseSocket()
        {
            serverSocket.Close();
        }
        private static void AcceptClient()
        {
            while (true)
            {
                try
                {
                    Socket clientsock = serverSocket.Accept();
                    ClientInfo client = new ClientInfo("", clientsock);
                    Thread clientThread = new Thread(Worker);
                    threadList.Add(clientThread);
                    clientThread.Start(client);
                }
                catch (SocketException )
                {
                    break;
                }
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
                        throw new ChatException("no nickname");
                    client.Nickname = msg.Nickname;
                    userList.Add(client.Nickname, client);
                    PublicMessage emsg = new PublicMessage("System", "Welcome " + msg.Nickname);
                    doPublicMessage(emsg);
                    AnnounceOnline(client.Nickname);
                    online = true;
                }
                catch (SocketException)
                {
                    online = false;
                }
                catch (ChatException e)
                {
                    doErrorMessage(client, e.Message);
                }
            }
            while (online && client.Client.Connected)
            {
                try
                {
                    string t = Common.doReceive(client.Client);
                    JObject json = (JObject)JsonConvert.DeserializeObject(t);
                    switch ((string)json.GetValue("MsgType"))
                    {
                        case "public": doPublicMessage(JsonConvert.DeserializeObject<PublicMessage>(t)); break;
                        case "private": doPrivateMessage(JsonConvert.DeserializeObject<PrivateMessage>(t)); break;
                        default: throw new ChatException("no such kind of message");

                    }
                }
                catch (SocketException)
                {
                    AnnounceOffline(client.Nickname);
                    userList.Remove(client.Nickname);
                    online = false;
                }
                catch (ChatException e)
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
                throw new ChatException("no such user");
            }
        }
        private static void doErrorMessage(ClientInfo client, string msg)
        {
            ErrorMessage emsg = new ErrorMessage(msg);
            string str = JsonConvert.SerializeObject(emsg).ToString();
           
            Common.doSend(client.Client, str);
        }

        public static void End()
        {
            Application.Exit();
        }

    }
}
