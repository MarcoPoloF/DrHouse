using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui;

namespace DocNoc.Xam
{
    [Activity(
        Label = "DocNoc",
        Icon = "@mipmap/icon",
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }
}
