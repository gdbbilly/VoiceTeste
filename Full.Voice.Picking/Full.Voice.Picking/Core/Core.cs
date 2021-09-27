using Android.Content;

namespace Voice.Picking.Test
{
    public class Core
    {
        private static Core instance;
        private Core(ITTS pTts, ISpeechRecognizerManager pSpeech, ILogger pLogger, Context pContext)
        {
            Speaker = pTts;
            Speaker.SetSpeechRate((float)0.9);

            Recognizer = pSpeech;

            Logger = pLogger;

            ContextApplication = pContext;

            BarcodeReaderApp = new BarcodeReaderLaser();
            BarcodeReaderApp.PopulateReaderPicker(pContext);
        }

        public Context ContextApplication { get; set; }

        public BarcodeReaderLaser BarcodeReaderApp { get; set; }

        public ILogger Logger { get; set; }
        public ITTS Speaker { get; set; }
        public ISpeechRecognizerManager Recognizer { get; set; }
        public string Address { get; set; }
        public string ViewName { get; set; }

        public static Core Instance
        {
            get
            {
                return instance;
            }
        }

        public static void SetInstance(ITTS pTts, ISpeechRecognizerManager pSpeech, ILogger pLogger, Android.Content.Context pContext)
        {
            instance = new Core(pTts, pSpeech, pLogger, pContext);
        }
    }
}
