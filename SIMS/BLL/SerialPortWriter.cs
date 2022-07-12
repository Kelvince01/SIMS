using System.Configuration;
using System.IO.Ports;

namespace SIMS.BLL
{
    public class SerialPortWriter
    {
        public static void WriteToPoleDisplay(string line1, string line2)
        {
            try
            {
                ConfigurationSettings.AppSettings["PoleDisplayName"].ToString();
                SerialPort serialPort = new SerialPort(ConfigurationSettings.AppSettings["PoleDisplayPort"].ToString(), 9600, Parity.None, 8, StopBits.One);
                if (serialPort == null)
                    return;
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Open();
                serialPort.DiscardInBuffer();
                serialPort.Write("                    ");
                serialPort.Write("                    ");
                serialPort.Write(line1);
                serialPort.Write(line2);
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
            catch
            {
            }
        }
    }
}
