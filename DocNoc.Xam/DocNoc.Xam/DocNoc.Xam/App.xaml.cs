using Openpay.Xamarin;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            //Registro de Licencia de Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTQ4NzAyQDMxMzkyZTMzMmUzMGlSZVJhVE1OcnBjOTA3SktaT21JYW1JMzRNTlJPdit3eGhrcjM5NDNXbkk9");
            
            InitializeComponent();

            VersionTracking.Track();

            //Al iniciar la aplicación, se emplea AppShell.Init() para obtener la p�gina de inicio.
            MainPage = AppShell.Init();
            //MainPage = new NavigationPage(AppShell.Init());
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
