using Microsoft.Maui.Handlers;

#if ANDROID
using Android.Views;
using AndroidX.AppCompat.Widget;
using DocNoc.Xam.Controls;
#endif

#if IOS
using UIKit;
using DocNoc.Xam.Controls;
#endif

namespace DocNoc.Xam.Handlers
{
    public partial class CalenderDatePickerHandler : DatePickerHandler
    {
        public CalenderDatePickerHandler()
        {
        }

#if ANDROID
        protected override AppCompatEditText CreatePlatformView()
        {
            var view = base.CreatePlatformView();
            view.SetBackground(null);
            view.SetTextColor(new Android.Graphics.Color(96, 106, 123));
            view.Gravity = GravityFlags.CenterVertical;
            view.SetPadding(0, 0, 0, 0);

            if (VirtualView is CalenderDatePicker picker && !string.IsNullOrEmpty(picker.PlaceHolderText))
            {
                view.Text = picker.PlaceHolderText;
            }

            return view;
        }
#endif

#if IOS
        protected override MauiDatePicker CreatePlatformView()
        {
            var view = base.CreatePlatformView();
            view.BorderStyle = UITextBorderStyle.None;
            view.VerticalAlignment = UIControlContentVerticalAlignment.Center;
            view.TextColor = new UIColor(96f / 255f, 106f / 255f, 123f / 255f, 1.0f);

            if (VirtualView is CalenderDatePicker picker && !string.IsNullOrEmpty(picker.PlaceHolderText))
            {
                view.Text = picker.PlaceHolderText;
            }

            return view;
        }
#endif
    }
}
