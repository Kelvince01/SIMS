using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LICENSE;
using Decoder = LICENSE.Decoder;
using Encoder = LICENSE.Encoder;

namespace SIMS.LICENSE
{
    /// <summary>
    /// Interaction logic for winLicenseExpire.xaml
    /// </summary>
    public partial class winLicenseExpire : Window
    {
        public winLicenseExpire()
        {
            InitializeComponent();
        }

        private void WinLicenseExpire_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.txtClientPcNo.Text = new INIFile().HddSerial("c:\\");
            this.txtClientPcNo.IsEnabled = false;
        }

        private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Decoder decoder = new Decoder();
                Encoder encoder = new Encoder();
                INIFile iniFile = new INIFile();
                string str1 = this.txtKey1.Text + this.txtKey2.Text + this.txtKey3.Text + this.txtKey4.Text + this.txtKey5.Text;
                if (str1.Length > 5)
                {
                    string lower = str1.ToLower();
                    string str2 = decoder.decryptData(lower);
                    if (str2.Length > 10)
                    {
                        string glic = str2.Substring(0, 2);
                        string str3 = str2.Substring(10);
                        string dateValue = str2.Substring(2, 8);
                        LicnenceVerifier licnenceVerifier = new LicnenceVerifier();
                        string date = licnenceVerifier.DatevalueToDate(dateValue);
                        DateTime now;
                        if (date.Length > 4)
                        {
                            DateTime dateTime1 = Convert.ToDateTime(date);
                            now = DateTime.Now;
                            DateTime dateTime2 = Convert.ToDateTime(now.ToString("MM-dd-yyyy"));
                            if (dateTime1 < dateTime2)
                                this.lblMsg.Text = "License Date is Over.Please, Renew Your License";
                        }
                        if (iniFile.HddSerial("c:\\") != str3)
                        {
                            this.lblMsg.Text = "License Number is Wrong";
                        }
                        else
                        {
                            iniFile.IniWriteValue("MS", "LICENSE", lower, licnenceVerifier.DataPath);
                            now = DateTime.Now;
                            string str4 = now.ToString("MM-dd-yyyy");
                            string str5 = encoder.EncryptData(glic + str4, glic);
                            iniFile.IniWriteValue("DT", "Date", str5, licnenceVerifier.DataPath);
                            int num = (int)MessageBox.Show("License activate successfully ");
                            this.Close();
                        }
                    }
                    else
                        this.lblMsg.Text = "License Number is Wrong";
                }
                else
                    this.lblMsg.Text = "Enter Licence Key";
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show("Invlaid license");
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WinLicenseExpire_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!e.IsDown || e.Key != Key.V)
                return;
            string str = Clipboard.GetData(TextDataFormat.Text.ToString()).ToString();
            if (str.Length == 19)
            {
                this.txtKey1.Text = str.Substring(0, 4);
                this.txtKey2.Text = str.Substring(4, 4);
                this.txtKey3.Text = str.Substring(8, 4);
                this.txtKey4.Text = str.Substring(12, 4);
                this.txtKey5.Text = str.Substring(16, 3);
            }
        }

        private void TxtKey1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Name != "txtKey5")
            {
                if (textBox.Text.Trim().Length <= 3)
                    return;
                //this.ProcessTabKey(true);
            }
            else if (textBox.Text.Trim().Length > 2)
                return;
            //this.ProcessTabKey(true);
        }
    }
}
