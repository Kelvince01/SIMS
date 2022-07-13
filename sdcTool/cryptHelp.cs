using System;
using System.Security.Cryptography;
using System.Text;

namespace sdcTool
{
    public class cryptHelp
    {
        public MD5 md5 = (MD5)new MD5CryptoServiceProvider();

        public string GenerateMD5(string txt)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.Default.GetBytes(txt);
                byte[] hash = md5.ComputeHash(bytes);
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < hash.Length; ++index)
                    stringBuilder.Append(hash[index].ToString("x2"));
                return stringBuilder.ToString();
            }
        }

        public string sha256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < hash.Length; ++index)
                stringBuilder.Append(hash[index].ToString("X2"));
            return stringBuilder.ToString();
        }

        public string AesEncrypt(string str, byte[] key)
        {
            int length = key.Length;
            if (string.IsNullOrEmpty(str))
                return (string)null;
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string base64String;
            try
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = key;
                rijndaelManaged.Mode = CipherMode.ECB;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                byte[] inArray = rijndaelManaged.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
                base64String = Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return base64String;
        }

        public string AesDecrypt(string str, byte[] key)
        {
            if (string.IsNullOrEmpty(str))
                return (string)null;
            byte[] inputBuffer = Convert.FromBase64String(str);
            string str1;
            try
            {
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Key = key;
                rijndaelManaged.Mode = CipherMode.ECB;
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                str1 = Encoding.UTF8.GetString(rijndaelManaged.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return str1;
        }

        public byte[] ConvertFrom(string strTemp)
        {
            try
            {
                if (Convert.ToBoolean(strTemp.Length & 1))
                    strTemp = "0" + strTemp;
                byte[] numArray = new byte[strTemp.Length / 2];
                for (int index = 0; index < strTemp.Length / 2; ++index)
                    numArray[index] = (byte)((int)strTemp[index * 2] - 48 << 4 | (int)strTemp[index * 2 + 1] - 48);
                return numArray;
            }
            catch
            {
                return (byte[])null;
            }
        }

        public byte[] ConvertFrom(string strTemp, int IntLen)
        {
            try
            {
                byte[] sourceArray = this.ConvertFrom(strTemp.Trim());
                byte[] destinationArray = new byte[IntLen];
                if (IntLen == 0)
                    return sourceArray;
                if (sourceArray.Length < IntLen)
                {
                    for (int index = 0; index < IntLen - sourceArray.Length; ++index)
                        destinationArray[index] = (byte)0;
                }
                Array.Copy((Array)sourceArray, 0, (Array)destinationArray, IntLen - sourceArray.Length, sourceArray.Length);
                return destinationArray;
            }
            catch
            {
                return (byte[])null;
            }
        }

        public string ConvertTo(byte[] AData)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder(AData.Length * 2);
                foreach (byte num in AData)
                {
                    stringBuilder.Append((int)num >> 4);
                    stringBuilder.Append((int)num & 15);
                }
                return stringBuilder.ToString();
            }
            catch
            {
                return (string)null;
            }
        }

        public byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] byteArray = new byte[s.Length / 2];
            for (int startIndex = 0; startIndex < s.Length; startIndex += 2)
                byteArray[startIndex / 2] = Convert.ToByte(s.Substring(startIndex, 2), 16);
            return byteArray;
        }
    }
}
