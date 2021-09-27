using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;


namespace Voice.Picking.Test.Pages.Extensions
{
    public static class Util
    {
        public static async Task ShowLoading(string titulo = "")
        {
            UserDialogs.Instance.ShowLoading(titulo);
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }

        public static async Task HideLoading()
        {
            UserDialogs.Instance.HideLoading();
            await Task.Delay(TimeSpan.FromMilliseconds(10));
        }

        public static void AdicionarResizeFontLabel(Label label, double minSize, double maxSize)
        {
            label.SizeChanged += (sender, args) =>
            {
                View view = (View)sender;
                if (view.Width <= 0 || view.Height <= 0)
                    return;

                FontCalc lowerFontCalc = new FontCalc(label, minSize, view.Width);
                FontCalc upperFontCalc = new FontCalc(label, maxSize, view.Width);

                while (upperFontCalc.FontSize - lowerFontCalc.FontSize > 1)
                {
                    double fontSize = (lowerFontCalc.FontSize + upperFontCalc.FontSize) / 2;
                    FontCalc newFontCalc = new FontCalc(label, fontSize, view.Width);
                    if (newFontCalc.TextHeight > view.Height)
                    {
                        upperFontCalc = newFontCalc;
                    }
                    else
                    {
                        lowerFontCalc = newFontCalc;
                    }
                }

                label.FontSize = lowerFontCalc.FontSize;
            };
        }
    }

    struct FontCalc
    {
        public FontCalc(Label label, double fontSize, double containerWidth) : this()
        {
            FontSize = fontSize;
            label.FontSize = fontSize;
            SizeRequest sizeRequest = label.GetSizeRequest(containerWidth, Double.PositiveInfinity);
            TextHeight = sizeRequest.Request.Height;
        }
        public double FontSize { private set; get; }
        public double TextHeight { private set; get; }
    }
}
