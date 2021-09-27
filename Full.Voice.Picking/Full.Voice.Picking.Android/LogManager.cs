using System;
using EnvironmentAndroid = Android.OS.Environment;
using System.IO;

namespace Voice.Picking.Test.Droid
{
    public class LogManager : ILogger
    {
        private static object lockobject = new object();

        private string PathLog()
        {
            var pathAndroid = EnvironmentAndroid.GetExternalStoragePublicDirectory(EnvironmentAndroid.DirectoryDownloads);

            var path = Path.Combine(pathAndroid.Path, "Log_VP.txt");

            return path;
        }
      
        public void Debug(string text)
        {
            Write(text, "DEBUG");
        }

        public void Error(string text)
        {
            Write(text, "ERROR");
        }

        public void Fatal(string text)
        {
            Write(text, "FATAL");
        }

        public void Info(string text)
        {
            Write(text, "INFO");
        }

        public void Trace(string text)
        {
            Write(text, "TRACE");
        }

        public void Warn(string text)
        {
            Write(text, "WARN");
        }

        private void Write(string text, string type)
        {
            lock (lockobject)
            {
                try
                {
                    var path = PathLog();
                    string s = $"{type}|{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}|{text}{Environment.NewLine}";
                    File.AppendAllText(path, s);
                }
                catch(Exception ex)
                {
                    // ?
                }
            }
        }
    }
}