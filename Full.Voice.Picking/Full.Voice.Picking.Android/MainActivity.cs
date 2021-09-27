using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using System;

namespace Voice.Picking.Test.Droid
{
    [Activity(Label = "Voice Teste", Name = "br.com.teste.voiceteste.MainActivity", Icon = "@drawable/appicon", Theme = "@style/MainTheme", MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = typeof(MainActivity).FullName;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);

            // inicializacao Acr.UserDialogs
            Acr.UserDialogs.UserDialogs.Init(this);

            // inicializacao ZXing
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            LoadApplication(new App(new TTSAndroid(BaseContext), new SpeechRecognizerManager(BaseContext, false), new LogManager(), this.ApplicationContext));
        }

        protected override void OnDestroy()
        {
            try
            {
                Core.Instance.Recognizer.Dispose();
            }
            catch (Exception){}
            try
            {
                Core.Instance.Speaker.Dispose();
            }
            catch (Exception) { }


            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}

