using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Acceso
{
    /// <summary>
    /// Definición de View (C#): Login (dn-04-3).
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Acceso.LoginPageViewModel;

            viewModel.OnAppearing();
        }
    }
}