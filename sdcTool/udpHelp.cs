using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace sdcTool
{
    public class udpHelp
    {
        public int sendMsg(string msg, int iPort, udpHelp.SomeFunWay sendCallBack)
        {
            try
            {
                this.recvCB = sendCallBack;
                this.udpcSend = new UdpClient(0);
                this.udpcSend.Client.IOControl(this.SIO_UDP_CONNRESET, new byte[]
                {
                    Convert.ToByte(false)
                }, null);
                byte[] sendbytes = Encoding.UTF8.GetBytes(msg);
                this.remoteEP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), iPort);
                int iRet = this.udpcSend.Available;
                this.udpcSend.Send(sendbytes, sendbytes.Length, this.remoteEP);
                this.udpReceiveState = new udpHelp.UdpState();
                this.udpReceiveState.ipEndPoint = this.remoteEP;
                this.udpReceiveState.udpClient = this.udpcSend;
                this.ReceiveMessages(this.udpReceiveState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return 0;
        }

        public void ReceiveMessages(udpHelp.UdpState udpReceiveState)
        {
            lock (this)
            {
                this.udpcSend.BeginReceive(new AsyncCallback(this.ReceiveCallback), udpReceiveState);
                this.receiveDone.WaitOne();
                Thread.Sleep(100);
            }
        }

        public void ReceiveCallback(IAsyncResult iar)
        {
            udpHelp.UdpState udpState = iar.AsyncState as udpHelp.UdpState;
            if (iar.IsCompleted)
            {
                try
                {
                    byte[] receiveBytes = udpState.udpClient.EndReceive(iar, ref this.udpReceiveState.ipEndPoint);
                    string receiveString = Encoding.UTF8.GetString(receiveBytes);
                    this.recvCB(0, receiveString);
                    udpState.udpClient.Close();
                    this.receiveDone.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public int startListen(int iPort, udpHelp.SomeFunWay sendCallBack)
        {
            this.recvCB = sendCallBack;
            IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("192.168.1.144"), iPort);
            this.udpcRecv = new UdpClient(localIpep);
            this.thrRecv = new Thread(new ParameterizedThreadStart(this.ReceiveMessage));
            this.thrRecv.Start();
            return 0;
        }

        public int stopListen()
        {
            this.thrRecv.Abort();
            this.udpcRecv.Close();
            return 0;
        }

        private string GetIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostName);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);
            for (; ; )
            {
                try
                {
                    byte[] bytRecv = this.udpcRecv.Receive(ref remoteIpep);
                    string message = Encoding.Unicode.GetString(bytRecv, 0, bytRecv.Length);
                    this.recvCB(1, "recv: " + message);
                }
                catch (Exception ex)
                {
                    this.recvCB(1, ex.Message);
                    break;
                }
            }
        }

        private const uint IOC_IN = 2147483648U;

        private UdpClient udpcSend = new UdpClient(0);

        private UdpClient udpcRecv = null;

        private Thread thrRecv = null;

        private ManualResetEvent sendDone = new ManualResetEvent(false);

        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        private IPEndPoint remoteEP = null;

        private static int IOC_VENDOR = 402653184;

        private int SIO_UDP_CONNRESET = (int)(unchecked((ulong)int.MinValue) | (ulong)((long)udpHelp.IOC_VENDOR) | 12UL);

        private udpHelp.UdpState udpReceiveState = null;

        public udpHelp.SomeFunWay recvCB;

        public class UdpState
        {
            public const int BufferSize = 2048;

            public UdpClient udpClient = null;

            public IPEndPoint ipEndPoint = null;

            public byte[] buffer = new byte[2048];

            public int counter = 0;
        }

        public delegate int SomeFunWay(int iRet, string strRt);
    }
}
