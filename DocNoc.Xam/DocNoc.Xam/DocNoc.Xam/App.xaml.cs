using Openpay.Xamarin;
using System;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam
{
    public partial class App : Application
    {
        private const string MerchantId = "mw5nbkzyunpjyrmcmb2w";
        private const string ApiKey = "pk_903db7effabc4e28896e8e26103c8e38";
        private const bool OpenPayProduccion = false;

        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            InitializeComponent();

            VersionTracking.Track();

            //Al iniciar la aplicación, se emplea AppShell.Init() para obtener la página de inicio.
            MainPage = AppShell.Init();
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Initialize Openpay
            if (CrossOpenpay.IsSupported)
            {
                CrossOpenpay.Current.Initialize(MerchantId, ApiKey, OpenPayProduccion);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
