using System.Runtime.InteropServices;
using System.Text;

namespace LICENSE
{
    public class INIFile
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string section,
            string key,
            string val,
            string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath);

        [DllImport("kernel32")]
        private static extern bool GetVolumeInformation(
            string Volume,
            StringBuilder VolumeName,
            int VolumeNameSize,
            out int SerialNumber,
            out uint SerialNumberLength,
            out uint flags,
            StringBuilder fs,
            int fs_size);

        public INIFile(string INIPath) => this.path = INIPath;

        public INIFile()
        {
        }

        public void IniWriteValue(string Section, string Key, string Value, string DataPath) => INIFile.WritePrivateProfileString(Section, Key, Value, DataPath);

        public string IniReadValue(string sSection, string sKey, string sDefault, string sINIFile)
        {
            StringBuilder retVal = new StringBuilder((int)byte.MaxValue);
            INIFile.GetPrivateProfileString(sSection, sKey, sDefault, retVal, (int)byte.MaxValue, sINIFile);
            return retVal.ToString();
        }

        public string HddSerial(string strDrive)
        {
            uint SerialNumberLength = 0;
            uint flags = 0;
            StringBuilder VolumeName = new StringBuilder((int)byte.MaxValue);
            StringBuilder fs = new StringBuilder((int)byte.MaxValue);
            int SerialNumber;
            INIFile.GetVolumeInformation(strDrive, VolumeName, (int)byte.MaxValue, out SerialNumber, out SerialNumberLength, out flags, fs, (int)byte.MaxValue);
            return SerialNumber.ToString();
        }
    }
}
