namespace HCI_Project.Library
{
    public static class LogService
    {
        public delegate void LogHandler(object message);
        public delegate void LogFormatHandler(string format, params object[] args);

        public static LogHandler Info { get; private set; }
        public static LogFormatHandler InfoFormat { get; private set; }
        public static LogHandler Warning { get; private set; }
        public static LogFormatHandler WarningFormat { get; private set; }
        public static LogHandler Error { get; private set; }
        public static LogFormatHandler ErrorFormat { get; private set; }

        public static void InitialService(LogHandler infoMethod, LogFormatHandler infoFormatMethod, LogHandler warningMethod, LogFormatHandler warningFormatMethod, LogHandler errorMethod, LogFormatHandler errorFormatMethod)
        {
            Info = infoMethod;
            InfoFormat = infoFormatMethod;
            Warning = warningMethod;
            WarningFormat = warningFormatMethod;
            Error = errorMethod;
            ErrorFormat = errorFormatMethod;
        }
    }
}
