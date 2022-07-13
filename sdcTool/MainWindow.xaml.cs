using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sdcTool.msgItems;

namespace sdcTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TCP_CONNECT_ERROR = 201;
        private const int TCP_CATCH_ERROR = 202;
        private SynchronizationContext mainThreadSynContext;
        private paramTCP m_tcpParam = new paramTCP();
        public JsonHelper m_jsonHelp = new JsonHelper();
        private udpHelp m_udpHelp = new udpHelp();
        private cryptHelp m_cryptHelp = new cryptHelp();
        private tcpHelp m_tcpHelp = new tcpHelp();
        private int m_iOffset = 0;
        private watchHelper watcher = new watchHelper();
        private watchHelper watcher2 = new watchHelper();
        private bool IsUdpcRecvStart = false;
        private byte[] m_keyByte;
        private Dictionary<string, string> devMap = new Dictionary<string, string>();
        private Dictionary<string, string> sdMap = new Dictionary<string, string>();
        private Dictionary<string, string> vatMap = new Dictionary<string, string>();

        public MainWindow()
        {
            LogHelper.Register();
            InitializeComponent();
            this.tcpBtn.IsEnabled = false;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Visibility = Visibility.Visible;
            MainWindow.UpdateConfig("port", this.portTCPTB.Text);
            MainWindow.GetConfig("port");
        }

        private static string GetConfig(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key] ?? "";
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error reading app settings");
            }
            return (string)null;
        }

        private static void UpdateConfig(string key, string value)
        {
            try
            {
                System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
                if (settings[key] == null)
                    settings.Add(key, value);
                else
                    settings[key].Value = value;
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public void sendMsgUDP(object obj)
        {
            paramSend paramSend = (paramSend)obj;
            this.showLogUI("send: " + paramSend.msg);
            this.m_udpHelp.sendMsg(paramSend.msg, paramSend.port, new udpHelp.SomeFunWay(this.recvFunUDP));
        }

        public int recvFunUDP(int iRet, string strRt)
        {
            if (0 == iRet)
            {
                this.showLogUI("recv:" + strRt);
                string str = this.m_cryptHelp.AesDecrypt(strRt, this.m_keyByte);
                this.showLogUI("recv plaintext:" + str);
                string[] strArray = str.Split('|');
                this.addDev(strArray[3] + "_" + strArray[2], strArray[1]);
            }
            else
                this.showLogUI(strRt);
            return 0;
        }

        private void addDev(string strKey, string data)
        {
            if (this.devMap.ContainsKey(strKey))
                return;
            this.devMap.Add(strKey, data);
            this.showLogUI("strKey: " + strKey + ", ip: " + data);
        }

        public int recvFunTCP(int iRet, string strRt)
        {
            if (0 == iRet)
            {
                this.showLogUI("recv:" + strRt);
                this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(this.getKey("123456"));
                this.showLogUI("recv plaintext:" + this.m_cryptHelp.AesDecrypt(strRt.Substring(6, strRt.Length - 6), this.m_keyByte));
                this.m_tcpHelp.closeClient();
                this.setEnabelUI(true);
            }
            else
                this.showLogUI(strRt);
            return 0;
        }

        private void showLogUI(string msg)
        {
            try
            {
                this.Dispatcher.Invoke(new EventHandler(delegate (object param0, EventArgs param1)
                {
                    if (this.recvTB.Text.Length == 0)
                        this.recvTB.Text = msg;
                    else
                        this.recvTB.Text = this.recvTB.Text + Environment.NewLine + msg;
                    this.recvTB.Focus();
                    this.recvTB.SelectionStart = this.recvTB.Text.Length;
                    this.recvTB.ScrollToLine(0);
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Save(nameof(showLogUI), ex.Message);
            }
        }

        private void setEnabelUI(bool bEnable)
        {
            try
            {
                this.Dispatcher.Invoke(new EventHandler(delegate (object param0, EventArgs param1)
                {
                    this.tcpBtn.IsEnabled = bEnable;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Save(nameof(setEnabelUI), ex.Message);
            }
        }

        private void setProgressUI(int value)
        {
            try
            {
                this.Dispatcher.Invoke(new EventHandler(delegate (object param0, EventArgs param1)
                {
                    this.progressBar1.Value = value;
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Save(nameof(setProgressUI), ex.Message);
            }
        }

        public void Delay(int mm)
        {
            DateTime now = DateTime.Now;
            while (now.AddMilliseconds((double)mm) > DateTime.Now)
                //Application.DoEvents();
                return;
        }

        private int findDev(string msg, int port)
        {
            int num = 1;
            for (int index = 0; index < num; ++index)
            {
                new Thread(new ParameterizedThreadStart(this.sendMsgUDP)).Start((object)new paramSend()
                {
                    port = port,
                    msg = msg
                });
                this.Delay(500);
            }
            return 0;
        }

        public string getKey(string data) => this.m_cryptHelp.sha256(this.m_cryptHelp.GenerateMD5(data).ToUpper());

        private int GetTcpParam()
        {
            if (0 == MainWindow.GetConfig("port").Length)
            {
                int num = (int)MessageBox.Show("pls input port.");
                this.portTCPTB.Focus();
                return -1;
            }
            if (0 == MainWindow.GetConfig("ip").Length)
            {
                int num = (int)MessageBox.Show("pls get ip first.");
                this.getIPBt.Focus();
                return -1;
            }
            this.m_tcpParam.ip = MainWindow.GetConfig("ip");
            this.m_tcpParam.port = int.Parse(MainWindow.GetConfig("port"));
            return 0;
        }

        public void sendMsgTCP(object obj)
        {
            paramTCP paramTcp = (paramTCP)obj;
            this.showLogUI("ip = " + paramTcp.ip + "; port = " + (object)paramTcp.port);
            if (!this.m_tcpHelp.SendConnection(paramTcp.ip, paramTcp.port, new tcpHelp.SomeFunWay(this.recvFunTCP)))
            {
                this.showLogUI("connect tcp server fail.");
            }
            else
            {
                tcpSendData tcpSendData = new tcpSendData()
                {
                    cashierID = "cashierName_Test1",
                    checkCode = "5e78e9cc4ec0db6d057725ea9c2ea10678ea70c3f85778a9af94826351f8bc66",
                    data = "{\"limit\":\"10\",\"offset\":\"0\",\"payType\":\"PAYTYPE_CASH\"}"
                };
                tcpSendData.checkCode = this.m_cryptHelp.sha256(tcpSendData.data).ToLower();
                tcpSendData.type = "SDCA0002";
                string str1 = JsonHelper.SerializeObjct((object)tcpSendData);
                string key = this.getKey("123456");
                this.showLogUI("src: " + str1);
                this.showLogUI("key: " + key);
                this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(key);
                string str2 = this.m_cryptHelp.AesEncrypt(str1, this.m_keyByte);
                this.showLogUI("AES Encrypt ret:" + str2);
                byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0:D6}", (object)str2.Length) + str2);
                this.m_tcpHelp.Send(bytes, bytes.Length);
                Thread.Sleep(2000);
            }
        }

        public byte[] packSendData(string strSrc)
        {
            string key = this.getKey("123456");
            this.showLogUI("src: " + strSrc);
            this.showLogUI("key: " + key);
            this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(key);
            string str = this.m_cryptHelp.AesEncrypt(strSrc, this.m_keyByte);
            this.showLogUI("AES Encrypt ret:" + str);
            return Encoding.UTF8.GetBytes(string.Format("{0:D6}", (object)str.Length) + str);
        }

        public string parseRecvData(string strRt)
        {
            this.showLogUI("recv:" + strRt);
            this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(this.getKey("123456"));
            string recvData = this.m_cryptHelp.AesDecrypt(strRt.Substring(6, strRt.Length - 6), this.m_keyByte);
            this.showLogUI("recv plaintext:" + recvData);
            return recvData;
        }

        private void beginGetSD()
        {
            this.m_iOffset = 0;
            this.sdMap.Clear();
            this.sdCB.Items.Clear();
            this.showLogUI("begin get SD.");
            this.doGetSD(this.m_iOffset);
        }

        private void doGetSD(int iOffset)
        {
            getSDReq parameter = new getSDReq()
            {
                cashierID = "11",
                checkCode = "5AEAD1D3D154FE1D39836A37023663DF9BE0DFEF9398EBD4141AA0A0A9DA07F3",
                data = JsonHelper.SerializeObjct((object)new getSDReq.dataItems()
                {
                    limit = "10",
                    offset = iOffset.ToString()
                }),
                type = "SDCA0013"
            };
            parameter.checkCode = this.m_cryptHelp.sha256(parameter.data).ToLower();
            this.mainThreadSynContext = SynchronizationContext.Current;
            new Thread(new ParameterizedThreadStart(this.getSDThread)).Start((object)parameter);
        }

        public void getSDThread(object obj)
        {
            byte[] by = this.packSendData(JsonHelper.SerializeObjct((object)(getSDReq)obj));
            if (!this.m_tcpHelp.SendConnection(this.m_tcpParam.ip, this.m_tcpParam.port, new tcpHelp.SomeFunWay(this.getSDThreadRespon)))
                this.showLogUI("connect tcp server fail. ip = " + this.m_tcpParam.ip + " port = " + (object)this.m_tcpParam.port);
            else
                this.m_tcpHelp.Send(by, by.Length);
        }

        public int getSDThreadRespon(int iRet, string strRt)
        {
            this.mainThreadSynContext.Post(new SendOrPostCallback(this.OnEndGetSDThread), (object)new threadStatus()
            {
                status = iRet,
                message = strRt
            });
            return 0;
        }

        private void OnEndGetSDThread(object state)
        {
            this.picUpdatingFormat.Visibility = Visibility.Visible;
            this.m_tcpHelp.closeClient();
            threadStatus threadStatus = (threadStatus)state;
            this.setEnableForm(true);
            this.showLogUI("end get SD. " + threadStatus.message);
            if (0 != threadStatus.status)
                return;
            getSDRes getSdRes = JsonHelper.JsonConvertObject<getSDRes>(this.parseRecvData(threadStatus.message));
            if (getSdRes.checkCode != this.m_cryptHelp.sha256(getSdRes.data).ToLower())
            {
                this.showLogUI("check code error.");
                this.showLogUI("end get SD.");
            }
            else if ("0000" != getSdRes.code)
            {
                this.showLogUI("get sd error.");
                this.showLogUI("end get SD. " + getSdRes.msg);
            }
            else
            {
                getSDRes.dataItems dataItems = JsonHelper.JsonConvertObject<getSDRes.dataItems>(getSdRes.data);
                for (int index = 0; index < dataItems.rates.Count; ++index)
                {
                    getSDRes.RatesItem rate = dataItems.rates[index];
                    string key = rate.serviceName + "_" + rate.sdRate + "_" + rate.id;
                    this.sdMap.Add(key, rate.id);
                    this.sdCB.Items.Add((object)key);
                }
                this.sdCB.SelectedIndex = this.sdCB.Items.IndexOf((object)this.sdMap.First<KeyValuePair<string, string>>().Key);
                if (this.sdMap.Count < int.Parse(dataItems.total))
                {
                    this.showLogUI("it requests sd continue.");
                    this.m_iOffset = this.sdMap.Count;
                    this.doGetSD(this.m_iOffset);
                }
                else
                {
                    this.showLogUI("end get SD." + getSdRes.msg);
                    this.picUpdatingFormat.Visibility = Visibility.Visible;
                    this.beginGetVat();
                }
            }
        }

        private void setEnableForm(bool bEnable)
        {
            this.getIPBt.IsEnabled = bEnable;
            this.tcpBtn.IsEnabled = bEnable;
            this.addGoodsBtn.IsEnabled = bEnable;
        }

        private void getVesrion()
        {
            this.setEnableForm(false);
            if (0 != this.GetTcpParam())
                this.setEnableForm(true);
            else
                this.doGetVersion();
        }

        private void doGetVersion()
        {
            getVersionReq parameter = new getVersionReq()
            {
                cashierID = "11",
                checkCode = "5AEAD1D3D154FE1D39836A37023663DF9BE0DFEF9398EBD4141AA0A0A9DA07F3",
                data = "SDCA0011",
                type = "SDCA0011"
            };
            parameter.checkCode = this.m_cryptHelp.sha256(parameter.data).ToLower();
            this.mainThreadSynContext = SynchronizationContext.Current;
            new Thread(new ParameterizedThreadStart(this.getVersionThread)).Start((object)parameter);
        }

        public void getVersionThread(object obj)
        {
            byte[] by = this.packSendData(JsonHelper.SerializeObjct((object)(getVersionReq)obj));
            if (!this.m_tcpHelp.SendConnection(this.m_tcpParam.ip, this.m_tcpParam.port, new tcpHelp.SomeFunWay(this.getVersionRespon)))
                this.showLogUI("connect tcp server fail. ip = " + this.m_tcpParam.ip + " port = " + (object)this.m_tcpParam.port);
            else
                this.m_tcpHelp.Send(by, by.Length);
        }

        public int getVersionRespon(int iRet, string strRt)
        {
            this.mainThreadSynContext.Post(new SendOrPostCallback(this.OnEndGetVersion), (object)new threadStatus()
            {
                status = iRet,
                message = strRt
            });
            return 0;
        }

        private void OnEndGetVersion(object state)
        {
            this.picUpdatingFormat.Visibility = Visibility.Visible;
            this.m_tcpHelp.closeClient();
            threadStatus threadStatus = (threadStatus)state;
            this.setEnableForm(true);
            this.showLogUI("end get SD. " + threadStatus.message);
            if (0 != threadStatus.status)
                return;
            getVersionRes getVersionRes = JsonHelper.JsonConvertObject<getVersionRes>(this.parseRecvData(threadStatus.message));
            if (getVersionRes.checkCode != this.m_cryptHelp.sha256(getVersionRes.data).ToLower())
            {
                this.showLogUI("check code error.");
                this.showLogUI("end get version");
            }
            else
            {
                getVersionRes.dataItems dataItems = JsonHelper.JsonConvertObject<getVersionRes.dataItems>(getVersionRes.data);
                if ("0000" != getVersionRes.code)
                    this.showLogUI("get version fail.");
                this.showLogUI("end get version. " + getVersionRes.msg);
                if (dataItems.rateCalculationMethod == "2")
                {
                    this.rateFomatLabel.Content = "SDCA0013";
                    this.picUpdatingFormat.Visibility = Visibility.Visible;
                    this.beginGetSD();
                }
                else
                {
                    this.rateFomatLabel.Content = "SDCA0002";
                    int num = (int)MessageBox.Show("It does not support now.");
                }
            }
        }

        private void beginGetVat()
        {
            this.m_iOffset = 0;
            this.vatMap.Clear();
            this.vatCB.Items.Clear();
            this.showLogUI("begin get vat.");
            this.doGetVat(this.m_iOffset);
        }

        private void doGetVat(int iOffset)
        {
            getTaxRateReq parameter = new getTaxRateReq()
            {
                cashierID = "11",
                checkCode = "5AEAD1D3D154FE1D39836A37023663DF9BE0DFEF9398EBD4141AA0A0A9DA07F3",
                data = JsonHelper.SerializeObjct((object)new getSDReq.dataItems()
                {
                    limit = "10",
                    offset = iOffset.ToString()
                }),
                type = "SDCA0012"
            };
            parameter.checkCode = this.m_cryptHelp.sha256(parameter.data).ToLower();
            this.mainThreadSynContext = SynchronizationContext.Current;
            new Thread(new ParameterizedThreadStart(this.getVatThread)).Start((object)parameter);
        }

        public void getVatThread(object obj)
        {
            byte[] by = this.packSendData(JsonHelper.SerializeObjct((object)(getTaxRateReq)obj));
            if (!this.m_tcpHelp.SendConnection(this.m_tcpParam.ip, this.m_tcpParam.port, new tcpHelp.SomeFunWay(this.getVatThreadRespon)))
                this.showLogUI("connect tcp server fail. ip = " + this.m_tcpParam.ip + " port = " + (object)this.m_tcpParam.port);
            else
                this.m_tcpHelp.Send(by, by.Length);
        }

        public int getVatThreadRespon(int iRet, string strRt)
        {
            this.mainThreadSynContext.Post(new SendOrPostCallback(this.OnEndGetVatThread), (object)new threadStatus()
            {
                status = iRet,
                message = strRt
            });
            return 0;
        }

        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            byte[] toHexByte = new byte[hexString.Length / 2];
            for (int index = 0; index < toHexByte.Length; ++index)
                toHexByte[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16);
            return toHexByte;
        }

        private void OnEndGetVatThread(object state)
        {
            this.picUpdatingFormat.Visibility = Visibility.Visible;
            this.m_tcpHelp.closeClient();
            threadStatus threadStatus = (threadStatus)state;
            this.setEnableForm(true);
            this.showLogUI("end get SD. " + threadStatus.message);
            if (0 != threadStatus.status)
                return;
            getTaxRateRes getTaxRateRes = JsonHelper.JsonConvertObject<getTaxRateRes>(this.parseRecvData(threadStatus.message));
            if (getTaxRateRes.checkCode != this.m_cryptHelp.sha256(getTaxRateRes.data).ToLower())
            {
                this.showLogUI("check code error.");
                this.showLogUI("end get vat.");
            }
            else if ("0000" != getTaxRateRes.code)
            {
                this.showLogUI("get vat error.");
                this.showLogUI("end get vat. " + getTaxRateRes.msg);
            }
            else
            {
                getTaxRateRes.dataItems dataItems = JsonHelper.JsonConvertObject<getTaxRateRes.dataItems>(getTaxRateRes.data);
                for (int index = 0; index < dataItems.rates.Count; ++index)
                {
                    getTaxRateRes.RatesItem rate = dataItems.rates[index];
                    string key = rate.serviceName + "_" + rate.vatRate + "_" + rate.id;
                    this.vatMap.Add(key, rate.id);
                    this.vatCB.Items.Add((object)key);
                }
                this.vatCB.SelectedIndex = this.vatCB.Items.IndexOf((object)this.vatMap.First<KeyValuePair<string, string>>().Key);
                if (this.vatMap.Count < int.Parse(dataItems.total))
                {
                    this.showLogUI("it requests sd continue.");
                    this.m_iOffset = this.vatMap.Count;
                    this.doGetVat(this.m_iOffset);
                }
                else
                    this.showLogUI("end get vat." + getTaxRateRes.msg);
            }
        }

        private void doInvoice(string name, string code, string sd, string vat)
        {
            this.watcher.begin(nameof(doInvoice));
            invoiceReq parameter = new invoiceReq()
            {
                cashierID = "11",
                checkCode = "5AEAD1D3D154FE1D39836A37023663DF9BE0DFEF9398EBD4141AA0A0A9DA07F3",
                data = JsonHelper.SerializeObjct((object)new invoiceReq.dataItems()
                {
                    buyerInfo = "1123132",
                    currency_code = "BDT",
                    payType = "PAYTYPE_CASH",
                    taskID = Guid.NewGuid().ToString("N"),
                    goodsInfo = new List<invoiceReq.GoodsInfoItem>()
          {
            new invoiceReq.GoodsInfoItem()
            {
              code = code,
              hsCode = "",
              item = name,
              price = "50",
              qty = "3",
              sd_category = sd,
              vat_category = vat
            }
          }
                }),
                type = "SDCA0000"
            };
            parameter.checkCode = this.m_cryptHelp.sha256(parameter.data).ToLower();
            this.mainThreadSynContext = SynchronizationContext.Current;
            new Thread(new ParameterizedThreadStart(this.invoiceThread)).Start((object)parameter);
        }

        public void invoiceThread(object obj)
        {
            byte[] by = this.packSendData(JsonHelper.SerializeObjct((object)(invoiceReq)obj));
            if (!this.m_tcpHelp.SendConnection(this.m_tcpParam.ip, this.m_tcpParam.port, new tcpHelp.SomeFunWay(this.invoiceThreadRespon)))
                this.showLogUI("connect tcp server fail. ip = " + this.m_tcpParam.ip + " port = " + (object)this.m_tcpParam.port);
            else
                this.m_tcpHelp.Send(by, by.Length);
        }

        public int invoiceThreadRespon(int iRet, string strRt)
        {
            this.watcher.end();
            this.mainThreadSynContext.Post(new SendOrPostCallback(this.OnEndInvoiceThread), (object)new threadStatus()
            {
                status = iRet,
                message = strRt
            });
            return 0;
        }

        private void OnEndInvoiceThread(object state)
        {
            this.m_tcpHelp.closeClient();
            threadStatus threadStatus = (threadStatus)state;
            this.setEnableForm(true);
            this.showLogUI("end invoice. " + threadStatus.message);
            if (0 != threadStatus.status)
                return;
            invoiceRes invoiceRes = JsonHelper.JsonConvertObject<invoiceRes>(this.parseRecvData(threadStatus.message));
            if (invoiceRes.checkCode != this.m_cryptHelp.sha256(invoiceRes.data).ToLower())
            {
                this.showLogUI("invoice error.");
                this.showLogUI("end invoice.");
            }
            else
            {
                this.watcher2.end();
                if ("0000" != invoiceRes.code)
                {
                    this.showLogUI("invoice error.");
                    this.showLogUI("end invoice. " + invoiceRes.msg);
                }
                else
                {
                    invoiceRes.dataItems dataItems = JsonHelper.JsonConvertObject<invoiceRes.dataItems>(invoiceRes.data);
                    for (int index = 0; index < dataItems.goodsInfo.Count; ++index)
                    {
                        invoiceRes.GoodsInfoItem goodsInfoItem = dataItems.goodsInfo[index];
                    }
                    this.showLogUI("end invoice." + invoiceRes.msg);
                }
            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (this.IsUdpcRecvStart)
            {
                this.m_udpHelp.stopListen();
                this.IsUdpcRecvStart = false;
            }
            LogHelper.UnRegister();
        }

        private void AddGoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (0 == this.nameTb.Text.Length)
            {
                int num = (int)MessageBox.Show("pls input name.");
                this.nameTb.Focus();
            }
            else if (0 == this.codeTb.Text.Length)
            {
                int num = (int)MessageBox.Show("pls input code.");
                this.codeTb.Focus();
            }
            else if (0 == this.sdMap.Count)
            {
                int num1 = (int)MessageBox.Show("pls get sd .");
            }
            else if (0 == this.vatMap.Count)
            {
                int num2 = (int)MessageBox.Show("pls get vat .");
            }
            else
            {
                if (MessageBox.Show("do you need invoice?", "szzt", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return;
                this.watcher2.begin("invoice");
                this.setEnableForm(false);
                this.doInvoice(this.nameTb.Text, this.codeTb.Text, this.sdCB.SelectedItem != null ? this.sdMap[this.sdCB.SelectedItem.ToString()] : this.sdMap.First<KeyValuePair<string, string>>().Value, this.vatCB.SelectedItem != null ? this.vatMap[this.vatCB.SelectedItem.ToString()] : this.vatMap.First<KeyValuePair<string, string>>().Value);
            }
        }

        private void TcpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.portTCPTB.Text.Length == 0)
            {
                int num = (int)MessageBox.Show("pls input port.");
                this.portTCPTB.Focus();
            }
            else if (this.devMap.Count == 0)
            {
                int num = (int)MessageBox.Show("pls get ip first.");
                this.getIPBt.Focus();
            }
            else
            {
                this.tcpBtn.IsEnabled = false;
                this.showLogUI("tcp test.");
                paramTCP parameter = new paramTCP();
                parameter.port = int.Parse(this.portTCPTB.Text);
                parameter.msg = "123";
                if (this.devCB.SelectedItem == null)
                {
                    KeyValuePair<string, string> keyValuePair = this.devMap.First<KeyValuePair<string, string>>();
                    parameter.ip = keyValuePair.Value;
                }
                else
                    parameter.ip = this.devMap[this.devCB.SelectedItem.ToString()];
                new Thread(new ParameterizedThreadStart(this.sendMsgTCP)).Start((object)parameter);
            }
        }

        private void GetIPBt_OnClick(object sender, RoutedEventArgs e)
        {
            this.watcher.begin("find sdc.");
            if (this.snTB.Text.Length == 0)
            {
                int num = (int)MessageBox.Show("pls input sn.");
                this.snTB.Focus();
            }
            else
            {
                this.setProgressUI(0);
                this.getIPBt.IsEnabled = false;
                this.setEnabelUI(false);
                this.devMap.Clear();
                this.devCB.Items.Clear();
                string key = this.getKey("SZZT002197");
                string str = "SZZT|" + this.snTB.Text;
                this.showLogUI("src: " + str);
                this.showLogUI("key: " + key);
                this.m_keyByte = this.m_cryptHelp.HexStringToByteArray(key);
                string msg = this.m_cryptHelp.AesEncrypt(str, this.m_keyByte);
                this.showLogUI("AES Encrypt ret:" + msg);
                int num1 = 1;
                int num2 = 12;
                while (num1 < num2)
                {
                    this.showLogUI("find sdc device  " + num1.ToString());
                    this.findDev(msg, int.Parse(this.remotePortTB.Text));
                    ++num1;
                    this.setProgressUI(100 / num2 * num1);
                    this.Delay(2000);
                }
                if (this.devMap.Count == 0)
                {
                    this.showLogUI("not find sdc dev, pls check network.");
                    this.getIPBt.IsEnabled = true;
                    this.setProgressUI(100);
                }
                else
                {
                    foreach (KeyValuePair<string, string> dev in this.devMap)
                        this.devCB.Items.Add((object)dev.Key);
                    this.devCB.SelectedIndex = this.devCB.Items.IndexOf((object)this.devMap.First<KeyValuePair<string, string>>().Key);
                    this.getIPBt.IsEnabled = true;
                    this.setEnabelUI(true);
                    this.setProgressUI(100);
                    this.watcher.end();
                }
            }
        }

        private void DevCB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.devMap.Count == 0)
            {
                int num = (int)MessageBox.Show("pls get ip first.");
                this.getIPBt.Focus();
            }
            else
            {
                MainWindow.UpdateConfig("ip", this.devMap[this.devCB.SelectedItem.ToString()]);
                MainWindow.UpdateConfig("port", this.portTCPTB.Text);
                if (MessageBox.Show("do you want to determine the initialization environment ?", "szzt", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                    return;
                if (0 != this.GetTcpParam())
                {
                    this.setEnableForm(true);
                }
                else
                {
                    this.picUpdatingFormat.Visibility = Visibility.Visible;
                    this.doGetVersion();
                }
            }
        }
    }
}
