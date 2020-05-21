using Serilog;
using System;

namespace SuperLog
{
    public static class Log
    {
        private static ILogger logger = null;

        private static string _FolderDirectory = Environment.CurrentDirectory;

        public static string FolderDirectory
        {
            get { return _FolderDirectory; }
            set
            {
                _FolderDirectory = value;
                InitLogger();
            }
        }

        static Log()
        {
            InitLogger();
        }

        private static void InitLogger()
        {
            logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console()
               .WriteTo.Debug()
               .WriteTo.Trace()
               .WriteTo.File($"{FolderDirectory}/Log/{DateTime.Now:yyyy-MM-dd}.log")
               .CreateLogger();
        }

        public static event Action<string> VerboseEvent;

        public static event Action<string> DebugEvent;

        public static event Action<string> InfoEvent;

        public static event Action<string> WarnEvent;

        public static event Action<string> ErrorEvent;

        public static event Action<string> FatalEvent;

        public static void Verbose(this string msg)
        {
            logger.Verbose(msg);
            VerboseEvent?.Invoke(msg);
        }

        public static void Debug(this string msg)
        {
            logger.Debug(msg);
            DebugEvent?.Invoke(msg);
        }

        public static void Info(this string msg)
        {
            logger.Information(msg);
            InfoEvent?.Invoke(msg);
        }

        public static void Warn(this string msg)
        {
            logger.Warning(msg);
            WarnEvent?.Invoke(msg);
        }

        public static void Error(this string msg)
        {
            logger.Error(msg);
            ErrorEvent?.Invoke(msg);
        }

        public static void Fatal(this string msg)
        {
            logger.Fatal(msg);
            FatalEvent?.Invoke(msg);
        }
    }
}
