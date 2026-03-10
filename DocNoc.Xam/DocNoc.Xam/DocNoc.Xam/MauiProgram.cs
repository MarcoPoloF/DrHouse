using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using DocNoc.Xam.Handlers;
using DocNoc.Xam.Custom.Maps;

namespace DocNoc.Xam
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Register Syncfusion license before creating the app
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "NTQ4NzAyQDMxMzkyZTMzMmUzMGlSZVJhVE1OcnBjOTA3SktaT21JYW1JMzRNTlJPdit3eGhrcjM5NDNXbkk9");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("UIFontIcons.ttf", "UIFontIcons");
                    fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                    fonts.AddFont("Montserrat-Medium.ttf", "MontserratMedium");
                    fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<Controls.BorderlessEntry, BorderlessEntryHandler>();
                    handlers.AddHandler<Controls.CalenderDatePicker, CalenderDatePickerHandler>();
#if ANDROID
                    handlers.AddHandler<CustomMap, Platforms.Android.CustomMapHandler>();
#elif IOS
                    handlers.AddHandler<CustomMap, Platforms.iOS.CustomMapHandler>();
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
