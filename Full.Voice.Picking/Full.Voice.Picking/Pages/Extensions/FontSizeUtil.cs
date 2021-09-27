using Xamarin.Forms;

namespace Voice.Picking.Test.Pages.Extensions
{
    public static class FontSizeUtil
    {
        private static double? _scale = null;

        private static double scale
        {
            get
            {
                if (_scale == null)
                {
                    _scale = 1;
                    if (Device.Idiom == TargetIdiom.Tablet)
                        _scale = 1.6;
                    if (Device.RuntimePlatform == Device.iOS)
                        _scale = _scale * 0.8;
                }
                return _scale.Value;
            }
        }

        public static double Micro => scale * 10;
        public static double Small => scale * 14;
        public static double Medium => scale * 18;
        public static double Large => scale * 22;
        public static double ExtraLarge => scale * 30;
        public static double Default => Small;
    }
}
