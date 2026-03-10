using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBarcode.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfCarousel.XForms.iOS;
using Syncfusion.SfDataGrid.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfPullToRefresh.XForms.iOS;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.Chat;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using UIKit;

namespace DocNoc.Xam.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Xamarin.FormsMaps.Init();

            SfSwitchRenderer.Init();
            SfBarcodeRenderer.Init();
            SfChatRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfCalendarRenderer.Init();
            SfPickerRenderer.Init();
            SfDatePickerRenderer.Init();
            SfPullToRefreshRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfDataGridRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfCardViewRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfExpanderRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfRatingRenderer.Init();
            SfComboBoxRenderer.Init();
            SfCarouselRenderer.Init();
            SfRotatorRenderer.Init();
            SfListViewRenderer.Init();

            Core.Init();

            SfGradientViewRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();

            LoadApplication(new App());

            //Soluci�n de tab bar transparente para iOS 15+
            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                //var backColor = UIColor.White;
                //var textColor = UIColor.FromRGB(33, 150, 234);

                //var stringAttributes = new UIStringAttributes { ForegroundColor = textColor };
                //UINavigationBar.Appearance.ScrollEdgeAppearance = new UINavigationBarAppearance
                //{
                //    BackgroundColor = backColor,
                //    TitleTextAttributes = stringAttributes,
                //    LargeTitleTextAttributes = stringAttributes
                //};

                UITabBar.Appearance.BackgroundColor = UIColor.White;
            }

            return base.FinishedLaunching(app, options);
        }
    }
}
