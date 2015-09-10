using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhihe.Smartplatform.SysTools
{
    public enum LogLevel
    {
        DEBUG,
        INFO,
        WARN,
        ERROR
    };


    public class Log
    {
        private static object m_lockObj = new object(); 
        private static string m_strLogFile = null;
        private static FileInfo m_file = null;
        private static StreamWriter m_writer = null;


        public static void InitLog(string _processName)
        {
            string strCurrentPath = Directory.GetCurrentDirectory();
            string strLogPath = strCurrentPath + "\\log\\";
            if (!Directory.Exists(strLogPath))
            {
                Directory.CreateDirectory(strLogPath);
            }


            m_strLogFile = strLogPath + _processName + "_" +
                DateTime.Now.ToString("yyyyMMdd") + ".log";


            m_file = new FileInfo(m_strLogFile);
            m_writer = m_file.AppendText();
        }


        public static void Error(string _msg)
        {
            log(LogLevel.ERROR, _msg);
        }


        public static void Error(string _className, string _msg)
        {
            string strMsg = _className + ": " + _msg;
            log(LogLevel.ERROR, strMsg);
        }


        public static void Debug(string _msg)
        {
            log(LogLevel.DEBUG, _msg);
        }


        public static void Debug(string _className, string _msg)
        {
            string strMsg = _className + ": " + _msg;
            log(LogLevel.DEBUG, strMsg);
        }


        public static void Info(string _msg)
        {
            log(LogLevel.INFO, _msg);
        }


        public static void Info(string _className, string _msg)
        {
            string strMsg = _className + ": " + _msg;
            log(LogLevel.INFO, strMsg);
        }


        public static void Warning(string _msg)
        {
            log(LogLevel.WARN, _msg);
        }


        private static void log(LogLevel _level, string _msg)
        {
            if (_msg == null || _msg == "")
            {
                return;
            }


            StringBuilder sb = new StringBuilder();
            // Time
            sb.Append("[");
            sb.Append(DateTime.Now.ToString());
            sb.Append("] ");
            // Level
            sb.Append("[");
            switch (_level)
            {
                case LogLevel.DEBUG:
                    sb.Append("DEBUG");
                    break;

                case LogLevel.INFO:
                    sb.Append("INFO");
                    break;


                case LogLevel.WARN:
                    sb.Append("WARN");
                    break;


                default:
                    sb.Append("ERROR");
                    break;
            }
            sb.Append("] ");
            // Content
            sb.Append(_msg);


            writeFile(sb.ToString());
        }


        private static void writeFile(string _strLine)
        {
            lock (m_lockObj)
            {
                try
                {
                    m_writer.WriteLine(_strLine);
                    m_writer.Flush();
                }
                catch (Exception)
                { }
            }
        }
    }
}
