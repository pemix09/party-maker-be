namespace Core.Models
{
    public class Log
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string LogType { get; set; }
        public string StackTrace { get; set; }
    }
}
