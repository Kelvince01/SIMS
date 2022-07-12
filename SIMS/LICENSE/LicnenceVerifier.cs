using System;
using System.IO;
using System.Windows;
using LICENSE;

namespace SIMS.LICENSE
{
    public class LicnenceVerifier
    {
        public string DataPath = "";
        private Decoder objDecoder = new Decoder();
        private Encoder objEncoder = new Encoder();
        private INIFile objINIFile = new INIFile();

        public LicnenceVerifier()
        {
            //this.DataPath = Application.StartupPath + "\\SYSTEMSC.INI";
            this.DataPath = AppDomain.CurrentDomain.BaseDirectory + "\\SYSTEMSC.INI";
            if (File.Exists(this.DataPath))
                return;
            File.Create(this.DataPath);
        }

        public bool checkNwrightDate(ref string msg)
        {
            string pass = this.objINIFile.IniReadValue("DT", "Date", "?", this.DataPath);
            if (pass.Trim() != "" && pass.Trim() != "?")
            {
                string str1 = this.objDecoder.decryptData(pass);
                DateTime dateTime1 = Convert.ToDateTime(str1.Substring(2));
                DateTime now = DateTime.Now;
                DateTime dateTime2 = Convert.ToDateTime(now.ToString("MM-dd-yyyy"));
                if (dateTime1 > dateTime2)
                {
                    msg = "You Change System Date Illegally, Invalid Date";
                    return false;
                }
                now = DateTime.Now;
                string str2 = now.ToString("MM-dd-yyyy");
                string glic = str1.Substring(0, 2);
                this.objINIFile.IniWriteValue("DT", "Date", this.objEncoder.EncryptData(glic + str2, glic), this.DataPath);
                return true;
            }
            msg = "License is Required. Please contact Mediasoft.";
            return false;
        }

        public string DatevalueToDate(string dateValue)
        {
            string str1 = dateValue.Substring(0, 4);
            string str2 = dateValue.Substring(6, 2);
            return dateValue.Substring(4, 4).Substring(0, 2) + "/" + str2 + "/" + str1;
        }

        public bool checkNwrightLicence(ref string msg)
        {
            string str1 = this.objDecoder.decryptData(this.objINIFile.IniReadValue("MS", "LICENSE", "?", this.DataPath));
            str1.Substring(0, 2);
            string str2 = str1.Substring(10);
            string date = this.DatevalueToDate(str1.Substring(2, 8));
            string str3 = this.objINIFile.HddSerial("c:\\");
            if (str2 != str3)
            {
                msg = "It is your wrong experiment.If You want to install it another computer then need to permission and license number from Mediasoft.Without permission it cannot be work properly.Please call your software company, Mediasoft.Thank you for YOUR Cooperation";
                return false;
            }
            if (date.Length > 4)
            {
                if (Convert.ToDateTime(date) < Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy")))
                {
                    msg = "License Date is Over.Please, Renew Your License";
                    return false;
                }
                int int32 = Convert.ToInt32((Convert.ToDateTime(date) - Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"))).TotalDays);
                if (int32 < 30)
                {
                    if (int32 == 0)
                    {
                        msg = "Your License is over Today.Please Call Mediasoft";
                        return false;
                    }
                    msg = "Your License will be over after  " + (object)int32 + "  days, Mediasoft";
                    return true;
                }
            }
            return true;
        }
    }
}
