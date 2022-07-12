using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using SIMS.Service;

namespace SIMS.BLL
{
    public class GlobalClass
    {
        private string _selectQuery;

        public static string RemoveSingleQuote(string data) => string.IsNullOrEmpty(data) ? string.Empty : data.Replace("'", "''");

        public static string RemoveSpace(string data) => string.IsNullOrEmpty(data) ? string.Empty : data.Trim();

        public static T RemoveSingleQuote<T>(object ob)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            T component = (T)ob;
            if ((object)component != null)
            {
                for (int index = 0; index < properties.Count; ++index)
                {
                    PropertyDescriptor propertyDescriptor = properties[index];
                    if (propertyDescriptor.PropertyType.Name == "String")
                    {
                        object obj = (object)GlobalClass.RemoveSpace(GlobalClass.RemoveSingleQuote((string)propertyDescriptor.GetValue((object)component)));
                        propertyDescriptor.SetValue((object)component, obj);
                    }
                }
            }
            return component;
        }

        public static string ConvertSystemDate(string date)
        {
            try
            {
                return date.Split('/')[1] + "/" + date.Split('/')[0] + "/" + date.Split('/')[2];
            }
            catch (Exception ex)
            {
                return "1/1/1900";
            }
        }

        public static string GetEncryptedPassword(string password)
        {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(password))));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2").ToLower());
            return stringBuilder.ToString();
        }

        public string GetMaxId(
          string coloumName,
          string rightStringFormat,
          string initialValue,
          string tableName)
        {
            Decimal num = 0M;
            this._selectQuery = "SELECT ISNULL(MAX(RIGHT(" + coloumName + ", " + (object)rightStringFormat.Length + ")) + 1, " + initialValue + ") AS maxID  FROM  " + tableName + " ";
            DataTable data = new SQLDAL().Select(this._selectQuery).Data;
            if (data != null && data.Rows.Count > 0)
                num = Decimal.Parse(data.Rows[0]["maxID"].ToString());
            return num.ToString(rightStringFormat);
        }

        public string GetMaxIdAccountCode(
          string coloumName,
          string rightStringLength,
          string initialValue,
          string tableName,
          string prefix,
          string parentCode)
        {
            string str1 = "";
            this._selectQuery = "SELECT ISNULL(MAX(RIGHT(" + coloumName + ", " + rightStringLength + ")) + 1, " + initialValue + ") AS maxID  FROM  " + tableName + " where ParentID='" + parentCode + "' ";
            DataTable data = new SQLDAL().Select(this._selectQuery).Data;
            if (data != null && data.Rows.Count > 0)
                str1 = data.Rows[0]["maxID"].ToString();
            if (str1.Length < initialValue.Length)
            {
                int num = initialValue.Length - str1.Length;
                string str2 = "";
                for (int index = 0; index < num; ++index)
                    str2 += "0";
                str1 = str2 + str1;
            }
            return prefix + str1;
        }

        public string GetMaxIdWithPrfix2(
          string coloumName,
          string rightStringLength,
          string initialValue,
          string tableName,
          string prefix)
        {
            string str1 = "";
            this._selectQuery = "SELECT ISNULL(MAX(CAST(SUBSTRING(" + coloumName + "," + (prefix.Length + 1).ToString() + ", " + rightStringLength + ") as int)) + 1, " + initialValue + ") AS maxID  FROM  " + tableName + " where left(" + coloumName + "," + (object)prefix.Length + ")='" + prefix + "' ";
            DataTable data = new SQLDAL().Select(this._selectQuery).Data;
            if (data != null && data.Rows.Count > 0)
                str1 = data.Rows[0]["maxID"].ToString();
            if (str1.Length < initialValue.Length)
            {
                int num = initialValue.Length - str1.Length;
                string str2 = "";
                for (int index = 0; index < num; ++index)
                    str2 += "0";
                str1 = str2 + str1;
            }
            return prefix + str1;
        }

        public string GetMaxIdWithPrfix(
          string coloumName,
          string rightStringLength,
          string initialValue,
          string tableName,
          string prefix)
        {
            string str1 = "";
            this._selectQuery = "SELECT ISNULL(MAX(RIGHT(" + coloumName + ", " + rightStringLength + ")) + 1, " + initialValue + ") AS maxID  FROM  " + tableName + " where left(" + coloumName + "," + (object)prefix.Length + ")='" + prefix + "' ";
            DataTable data = new SQLDAL().Select(this._selectQuery).Data;
            if (data != null && data.Rows.Count > 0)
                str1 = data.Rows[0]["maxID"].ToString();
            if (str1.Length < initialValue.Length)
            {
                int num = initialValue.Length - str1.Length;
                string str2 = "";
                for (int index = 0; index < num; ++index)
                    str2 += "0";
                str1 = str2 + str1;
            }
            return prefix + str1;
        }

        public string GetMaxId(string coloumName, string tableName)
        {
            string str = "";
            this._selectQuery = "SELECT  MAX(" + coloumName + ") +1 AS " + coloumName + " FROM " + tableName;
            DataTable data = new SQLDAL().Select(this._selectQuery).Data;
            if (data != null && data.Rows.Count > 0)
                str = data.Rows[0][coloumName].ToString();
            return string.IsNullOrEmpty(str) ? "1" : str;
        }

        public static string GetMacAddress()
        {
            string macAddress = "";
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.GetPhysicalAddress().ToString().Length > 5)
                    return networkInterface.GetPhysicalAddress().ToString();
                if (networkInterface.OperationalStatus == OperationalStatus.Down && networkInterface.GetPhysicalAddress().ToString().Length > 5)
                    return networkInterface.GetPhysicalAddress().ToString();
            }
            return macAddress;
        }

        public static string GetMacAddressAll()
        {
            string macAddressAll = "'-1'";
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.GetPhysicalAddress().ToString().Length > 5)
                    macAddressAll = macAddressAll + ",'" + networkInterface.GetPhysicalAddress().ToString() + "'";
                if (networkInterface.OperationalStatus == OperationalStatus.Down && networkInterface.GetPhysicalAddress().ToString().Length > 5)
                    macAddressAll = macAddressAll + ",'" + networkInterface.GetPhysicalAddress().ToString() + "'";
            }
            return macAddressAll;
        }

        public static string Encrypt(string clearText)
        {
            string password = "MSDSL2012";
            byte[] bytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
                {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                });
                ((SymmetricAlgorithm)aes).Key = rfc2898DeriveBytes.GetBytes(32);
                ((SymmetricAlgorithm)aes).IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, ((SymmetricAlgorithm)aes).CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.Close();
                    }
                    clearText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string password = "MSDSL2012";
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, new byte[13]
                {
          (byte) 73,
          (byte) 118,
          (byte) 97,
          (byte) 110,
          (byte) 32,
          (byte) 77,
          (byte) 101,
          (byte) 100,
          (byte) 118,
          (byte) 101,
          (byte) 100,
          (byte) 101,
          (byte) 118
                });
                ((SymmetricAlgorithm)aes).Key = rfc2898DeriveBytes.GetBytes(32);
                ((SymmetricAlgorithm)aes).IV = rfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, ((SymmetricAlgorithm)aes).CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(buffer, 0, buffer.Length);
                        cryptoStream.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(memoryStream.ToArray());
                }
            }
            return cipherText;
        }

        public static string GetExceptionStringMsg(Exception ex)
        {
            DbEntityValidationException validationException = ex as DbEntityValidationException;
            string exceptionStringMsg = ex.Message + ex.StackTrace;
            if (validationException != null)
            {
                foreach (DbEntityValidationResult entityValidationError in validationException.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in (IEnumerable<DbValidationError>)entityValidationError.ValidationErrors)
                        exceptionStringMsg += validationError.ErrorMessage;
                }
            }
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
                exceptionStringMsg = exceptionStringMsg + Environment.NewLine + ex.InnerException.InnerException.Message;
            return exceptionStringMsg;
        }
    }
}
