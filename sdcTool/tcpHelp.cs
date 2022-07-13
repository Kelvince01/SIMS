using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace sdcTool
{
    public class tcpHelp
    {
        public Socket tcpClientObj;
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private byte[] m_ReceiverByte = new byte[204800];
        private tcpHelp.SomeFunWay m_callFun;

        public bool SendConnection(string ip, int port, tcpHelp.SomeFunWay CBFun)
        {
            this.m_callFun = CBFun;
            IPAddress address = IPAddress.Parse(ip);
            this.tcpClientObj = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            bool flag = false;
            do
            {
                try
                {
                    this.tcpClientObj.Connect(address, port);
                }
                catch (Exception ex)
                {
                    int num = this.m_callFun(-1, ex.Message);
                }
            }
            while (flag);
            try
            {
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.Completed += new EventHandler<SocketAsyncEventArgs>(this.MyCompleted);
                e.SetBuffer(this.m_ReceiverByte, 0, this.m_ReceiverByte.Length);
                this.tcpClientObj.ReceiveAsync(e);
            }
            catch (Exception ex)
            {
                int num = this.m_callFun(-1, ex.Message);
                return false;
            }
            return true;
        }

        private void MyCompleted(object sender, SocketAsyncEventArgs e)
        {
            ((IPEndPoint)(sender as Socket).RemoteEndPoint).Address.ToString();
            try
            {
                if (e.SocketError != SocketError.Success || e.BytesTransferred <= 0 || e.Buffer.Length <= 0)
                    return;
                byte[] numArray = new byte[e.BytesTransferred];
                Array.Copy((Array)e.Buffer, (Array)numArray, e.BytesTransferred);
                int num = this.m_callFun(0, Encoding.Default.GetString(numArray));
            }
            catch (Exception ex)
            {
                LogHelper.Save(nameof(MyCompleted), ex.Message);
            }
        }

        public void Send(byte[] by, int length)
        {
            if (this.tcpClientObj == null || !this.tcpClientObj.Connected)
                return;
            this.tcpClientObj.Send(by, length, SocketFlags.None);
        }

        public void closeClient()
        {
            if (!this.tcpClientObj.Connected)
                return;
            this.tcpClientObj.Shutdown(SocketShutdown.Both);
            this.tcpClientObj.Close();
        }

        public delegate bool receiveDelegate(byte[] receiveData);

        public delegate int SomeFunWay(int iRet, string strRt);
    }
}
