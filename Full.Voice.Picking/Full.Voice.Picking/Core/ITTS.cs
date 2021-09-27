namespace Voice.Picking.Test
{
    public interface ITTS
    {
        void Speak(string text);

        void SetSpeechRate(float speechRate);

        bool IsSpeaking();

        void Stop();

        void Dispose();
    }
}
