
namespace Voice.Picking.Test
{
    public interface ILogger
    {
        void Trace(string text);
        void Debug(string text);
        void Info(string text);
        void Warn(string text);
        void Error(string text);
        void Fatal(string text);
    }
}
