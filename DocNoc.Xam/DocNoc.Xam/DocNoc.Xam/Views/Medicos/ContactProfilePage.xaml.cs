using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Medicos
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactProfilePage
    {
        public ContactProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Medicos.ContactProfileViewModel;

            viewModel.OnAppearing();
        }
    }
}