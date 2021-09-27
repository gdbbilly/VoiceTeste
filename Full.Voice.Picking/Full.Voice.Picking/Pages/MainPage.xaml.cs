using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Collections.Generic;

namespace Voice.Picking.Test.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public bool Listening { get; set; }
        public MainPage()
        {
            InitializeComponent();

            switchSpeak.Toggled += SwitchSpeak_Toggled;
            Listening = false;
        }

        private void SwitchSpeak_Toggled(object sender, ToggledEventArgs e)
        {
            Listening = e.Value;
            ListeningInitialize();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Core.Instance.Speaker.Speak("Bem vindo");

            ListeningInitialize();
        }

        protected void ListeningInitialize()
        {
            if (!Core.Instance.Recognizer.IsmIsListening() && Listening)
            {
                Core.Instance.Recognizer.StartListening();
            }
            if (!Listening)
            {
                Core.Instance.Recognizer.StopListening();
            }

            Core.Instance.Recognizer.ResultsChanged -= SpeechValidatePage_ResultsChanged;
            Core.Instance.Recognizer.ResultsChanged += SpeechValidatePage_ResultsChanged;
        } 


        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void SpeechValidatePage_ResultsChanged(IList<string> results)
        {
            txtOut.Text = results.FirstOrDefault();
            //if (results.Any(x => x.Equals("repita", StringComparison.CurrentCultureIgnoreCase))
            //     || results.Any(x => x.Equals("repite", StringComparison.CurrentCultureIgnoreCase))
            //     || results.Any(x => x.Equals("repitir", StringComparison.CurrentCultureIgnoreCase))
            //     || results.Any(x => x.Equals("repetir", StringComparison.CurrentCultureIgnoreCase)))
            //{
                
            //}

        }

        private async void BtnVoltar_OnClicked(object sender, EventArgs e)
        {
            Core.Instance.Speaker.Speak(txtCampo.Text);
        }
    }
}
