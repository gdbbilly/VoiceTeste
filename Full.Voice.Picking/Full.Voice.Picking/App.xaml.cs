using Android.Content;
using Xamarin.Forms;

namespace Voice.Picking.Test
{
    public partial class App : Application
    {
        private static ITTS _pTts;
        private static ISpeechRecognizerManager _pSpeech;
        private static ILogger _pLogger;

        public App(ITTS pTts, ISpeechRecognizerManager pSpeech, ILogger pLogger, Context pContext)
        {
            InitializeComponent();
            Core.SetInstance(pTts, pSpeech, pLogger, pContext);

            _pTts = pTts;
            _pSpeech = pSpeech;
            _pLogger = pLogger;

            _pLogger.Info($"App Start - {System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
