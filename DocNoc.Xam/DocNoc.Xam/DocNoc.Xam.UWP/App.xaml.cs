using Syncfusion.SfRotator.XForms.UWP;
using Syncfusion.XForms.UWP.Border;
using Syncfusion.XForms.UWP.ComboBox;
using Syncfusion.SfBarcode.XForms.UWP;
using Syncfusion.XForms.UWP.Chat;
using Syncfusion.SfBusyIndicator.XForms.UWP;
using Syncfusion.XForms.UWP.Buttons;
using Syncfusion.SfPicker.XForms.UWP;
using Syncfusion.SfPullToRefresh.XForms.UWP;
using Syncfusion.ListView.XForms.UWP;
using Syncfusion.XForms.UWP.ProgressBar;
using Syncfusion.SfDataGrid.XForms.UWP;
using Syncfusion.XForms.UWP.PopupLayout;
using Syncfusion.XForms.UWP.TextInputLayout;
using Syncfusion.SfCarousel.XForms.UWP;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Syncfusion.XForms.Pickers.UWP;

namespace DocNoc.Xam.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //Registro de Licencia de Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjU4NTA5QDMxMzgyZTMxMmUzMGhrUi9oSjlCaWlRWjJnYTRsOTdXckh5VzV4Y1dnY0NtRVFzOUJuK0I3RmM9");

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {


            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                //Xamarin.Shell
                global::Xamarin.Forms.Forms.SetFlags("Shell_UWP_Experimental");

List<Assembly> assembliesToInclude = new List<Assembly>();
assembliesToInclude.Add(typeof(SfRotatorRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfBorderRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfComboBoxRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfBarcodeRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfChatRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfBusyIndicatorRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfCheckBoxRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfRadioButtonRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfTimePickerRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfPickerRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfPullToRefreshRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfListViewRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfLinearProgressRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfDataGridRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfTextInputLayoutRenderer).GetTypeInfo().Assembly);
assembliesToInclude.Add(typeof(SfCarouselRenderer).GetTypeInfo().Assembly);
Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
