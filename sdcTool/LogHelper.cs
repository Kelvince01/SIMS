using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace sdcTool
{
    internal class LogHelper
    {
        private static readonly ConcurrentQueue<LogHelper.FlashLogMessage> _que = new ConcurrentQueue<LogHelper.FlashLogMessage>();
        private static readonly ManualResetEvent _mre = new ManualResetEvent(false);
        private static bool mRunFlag = false;
        private static string mTag = " [log] ";

        public static void Save(string sTag, string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (FileStream fileStream = new FileStream(path + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append))
            {
                using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    streamWriter.WriteLine(DateTime.Now.ToString() + " [" + sTag + "] " + message);
            }
        }

        public static void Log(string sTag, string msg)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (FileStream fileStream = new FileStream(path + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append))
            {
                using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                    streamWriter.WriteLine(DateTime.Now.ToString("yyyyMMdd HHmmss") + sTag + msg);
            }
        }

        public static void Register()
        {
            LogHelper.mRunFlag = true;
            new Thread(new ThreadStart(LogHelper.WriteLog))
            {
                IsBackground = false
            }.Start();
            LogHelper.Log(LogHelper.mTag, "log thread start.");
        }

        public static void UnRegister()
        {
            LogHelper.mRunFlag = false;
            LogHelper._mre.Set();
        }

        private static void WriteLog()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\log\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            while (LogHelper.mRunFlag)
            {
                LogHelper._mre.WaitOne();
                LogHelper.FlashLogMessage flashLogMessage;
                while (LogHelper._que.Count > 0 && LogHelper._que.TryDequeue(out flashLogMessage))
                {
                    using (FileStream fileStream = new FileStream(path + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)fileStream))
                            streamWriter.WriteLine(flashLogMessage.sHead + flashLogMessage.sData);
                    }
                }
                LogHelper._mre.Reset();
                Thread.Sleep(1);
            }
            LogHelper.Log(LogHelper.mTag, "log thread stop.");
        }

        public class FlashLogMessage
        {
            public string sHead;
            public string sData;
        }
    }
}
