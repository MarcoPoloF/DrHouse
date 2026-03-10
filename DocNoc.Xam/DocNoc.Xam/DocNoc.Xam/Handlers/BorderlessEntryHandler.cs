using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

#if ANDROID
using Android.Views;
using AndroidX.AppCompat.Widget;
#endif

#if IOS
using UIKit;
#endif

namespace DocNoc.Xam.Handlers
{
    public partial class BorderlessEntryHandler : EntryHandler
    {
        public BorderlessEntryHandler()
        {
        }

#if ANDROID
        protected override AppCompatEditText CreatePlatformView()
        {
            var view = base.CreatePlatformView();
            view.SetBackground(null);
            view.Gravity = GravityFlags.CenterVertical;
            view.SetPadding(0, 0, 0, 0);
            return view;
        }
#endif

#if IOS
        protected override MauiTextField CreatePlatformView()
        {
            var view = base.CreatePlatformView();
            view.BorderStyle = UITextBorderStyle.None;
            return view;
        }
#endif
    }
}
