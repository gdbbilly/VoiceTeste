using Android.Content;
using Android.Media;
using Android.Speech;
using System;
using System.Collections.Generic;
using Android.OS;
using Java.Lang;
using Android.Runtime;

namespace Voice.Picking.Test.Droid
{
    public class SpeechRecognizerManager : Java.Lang.Object, IRecognitionListener, ISpeechRecognizerManager
    {
        protected AudioManager mAudioManager;
        protected SpeechRecognizer mSpeechRecognizer;
        protected Intent mSpeechRecognizerIntent;

        protected bool mIsListening;
        private bool mIsStreamSolo;
        private bool mMute = false;
        public event Action<IList<string>> ResultsChanged;

        public SpeechRecognizerManager(Context context, bool hasInternet)
        {
            mAudioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            mSpeechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(context);

            mSpeechRecognizer.SetRecognitionListener(this);
            mSpeechRecognizerIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraCallingPackage, context.PackageName);

            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 25000);
            //mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraWebSearchOnly, false);
            //mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1000);


            if (!hasInternet)
            {
                mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraPreferOffline, true);
            }
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 10);
            mSpeechRecognizerIntent.PutExtra(RecognizerIntent.ExtraLanguagePreference, "pt-BR");
        }

        public void ClearEventResults()
        {
            ResultsChanged = delegate { };
        }

        public void ListenAgain()
        {
            if (mIsListening)
            {
                mIsListening = false;
                mSpeechRecognizer.Cancel();
                StartListening();
            }
        }

        public void StartListening()
        {
            if (!mIsListening)
            {
                mIsListening = true;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
                {
                    // turn off beep sound
                    if (!mIsStreamSolo && mMute)
                    {
                        mAudioManager.SetStreamMute(Stream.Notification, true);
                        mAudioManager.SetStreamMute(Stream.Alarm, true);
                        mAudioManager.SetStreamMute(Stream.Music, true);
                        mAudioManager.SetStreamMute(Stream.Ring, true);
                        mAudioManager.SetStreamMute(Stream.System, true);
                        mIsStreamSolo = true;
                    }
                }
                mSpeechRecognizer.StartListening(mSpeechRecognizerIntent);
            }
        }

        public void Destroy()
        {
            mIsListening = false;
            if (!mIsStreamSolo)
            {
                mAudioManager.SetStreamMute(Stream.Notification, false);
                mAudioManager.SetStreamMute(Stream.Alarm, false);
                mAudioManager.SetStreamMute(Stream.Music, false);
                mAudioManager.SetStreamMute(Stream.Ring, false);
                mAudioManager.SetStreamMute(Stream.System, false);
                mIsStreamSolo = true;
            }

            if (mSpeechRecognizer != null)
            {
                mSpeechRecognizer.StopListening();
                mSpeechRecognizer.Cancel();
                mSpeechRecognizer.Destroy();
                mSpeechRecognizer = null;
            }

        }

        public new void Dispose()
        {
            Destroy();
            mSpeechRecognizer.Dispose();
        }

        public void StopListening()
        {
            mIsListening = false;
            mSpeechRecognizer.StopListening();
        }

        public bool IsmIsListening()
        {
            return mIsListening;
        }

        public void OnBeginningOfSpeech()
        {
            //throw new NotImplementedException();
        }

        public void OnBufferReceived(byte[] buffer)
        {
            // throw new NotImplementedException();
        }

        public void OnEndOfSpeech()
        {
            //throw new NotImplementedException();
        }

        public void OnError([GeneratedEnum] SpeechRecognizerError error)
        {

            if (error == SpeechRecognizerError.RecognizerBusy)
            {
                //throw new System.Exception("ERROR RECOGNIZER BUSY");
            }

            if (error == SpeechRecognizerError.NoMatch)
            {
                //throw new System.Exception("ErrorNoMatch");
            }

            if (error == SpeechRecognizerError.Network)
            {
                // throw new System.Exception("STOPPED LISTENING");
            }

            Thread.Sleep(100);
            ListenAgain();
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            //throw new NotImplementedException();
        }

        public void OnPartialResults(Bundle partialResults)
        {
            //throw new NotImplementedException();
        }

        public void OnReadyForSpeech(Bundle @params)
        {
            // throw new NotImplementedException();
        }

        public void OnRmsChanged(float rmsdB)
        {
            // throw new NotImplementedException();
        }

        public void OnResults(Bundle results)
        {
            if (results != null)
            {
                onResults(results.GetStringArrayList(SpeechRecognizer.ResultsRecognition));
            }
            ListenAgain();
        }

        public void onResults(IList<string> results)
        {
            if (results != null && results.Count > 0)
            {
                if (ResultsChanged != null)
                {
                    ResultsChanged.Invoke(results);
                }
            }
        }
    }
}