using System;
using System.Collections.Generic;

namespace Voice.Picking.Test
{
    public interface ISpeechRecognizerManager
    {
        event Action<IList<string>> ResultsChanged;
        void ClearEventResults();

        void ListenAgain();

        void StartListening();

        void Destroy();

        void Dispose();

        void StopListening();

        bool IsmIsListening();

        void OnBeginningOfSpeech();

        void OnBufferReceived(byte[] buffer);

        void OnEndOfSpeech();

        void onResults(IList<string> results);
    }
}
