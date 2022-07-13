using System;
using System.Text;

namespace sdcTool
{
    internal class watchHelper
    {
        private DateTime beginTime;
        private StringBuilder mTag = new StringBuilder(256);

        public void begin(string sTag)
        {
            this.mTag.Clear();
            this.mTag.Append(sTag);
            this.beginTime = DateTime.Now;
            LogHelper.Save(this.mTag.ToString(), "begin " + this.beginTime.ToString("yyyyMMddHHmmssfff"));
        }

        public void end()
        {
            DateTime now = DateTime.Now;
            LogHelper.Save(this.mTag.ToString(), "end " + now.ToString("yyyyMMddHHmmssfff"));
            TimeSpan timeSpan = now - this.beginTime;
            LogHelper.Save(this.mTag.ToString(), "used " + (object)timeSpan.Milliseconds + "ms.");
        }
    }
}
