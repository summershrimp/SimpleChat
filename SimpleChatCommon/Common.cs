using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SimpleChatCommon
{
    public static class Common
    {
        public static void doSend(Socket sock, string msg)
        {
            string send = "\x1" + msg + "\x2";
            byte[] sendbuffer = System.Text.Encoding.UTF8.GetBytes(send);
            sock.Send(sendbuffer);
        }

        private static string recvbuf = "";
        private static bool ondoing = false;
        public static string doReceive(Socket sock)
        {
            byte[] recv = new byte[2048];
            sock.Receive(recv);
            recvbuf += System.Text.Encoding.UTF8.GetString(recv);
            string t = "";
            if (!ondoing)
            {
                int i;
                for (i = 0; i < recvbuf.Length; i++)
                {
                    if (recvbuf[i] == '\x1')
                    {
                        t = "";
                        ondoing = true;
                        break;
                    }
                }
                i++;
                for (; i < recvbuf.Length; ++i)
                {
                    if (recvbuf[i] == '\x2')
                    {
                        ondoing = false;
                        recvbuf = recvbuf.Substring(i + 1 );
                        break;
                    }
                    t += recvbuf[i];
                }
                if (i == recvbuf.Length)
                {
                    recvbuf = "";
                }
            }
            else
            {
                int i = 0;
                for (; i < recvbuf.Length; ++i)
                {
                    if (recvbuf[i] == '\x2')
                    {
                        ondoing = false;
                        recvbuf = recvbuf.Substring(i + 1);
                        break;
                    }
                    t += recvbuf[i];
                }
                if (i == recvbuf.Length)
                {
                    recvbuf = "";
                }
            }
            return t;
        }
    }
}
