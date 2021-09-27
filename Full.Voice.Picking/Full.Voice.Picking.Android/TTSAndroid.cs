using System.Collections.Generic;
using Android.Content;
using Android.Speech.Tts;
using Voice.Picking.Test;

namespace Voice.Picking.Test.Droid
{
    public class TTSAndroid : Java.Lang.Object, ITTS, TextToSpeech.IOnInitListener
    {
        private IDictionary<string, string> _paramMap = new Dictionary<string, string>();

        TextToSpeech speaker;
        public TTSAndroid(Context context)
        {
            speaker = new TextToSpeech(context, this);
        }

        public void Speak(string text)
        {
            speaker.Speak(text, QueueMode.Flush, _paramMap);
            while (IsSpeaking()) { }
        }

        public void SetSpeechRate(float speechRate)
        {
            speaker.SetSpeechRate(speechRate);
        }

        void TextToSpeech.IOnInitListener.OnInit(OperationResult status)
        {
            //Console.writeln("TTS on Init status is " + status);
        }

        public bool IsSpeaking()
        {
            return speaker.IsSpeaking;
        }

        public void Stop()
        {
            speaker.Stop();
            speaker.Shutdown();
        }

        public new void Dispose()
        {
            Stop();
            speaker.Dispose();
        }
    }
}