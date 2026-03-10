using DocNoc.Xam.DataService;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.EssentialUI
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationListCardPage
    {
        public NavigationListCardPage()
        {
            InitializeComponent();
            this.BindingContext = NavigationDataService.Instance.NavigationViewModel;
        }
    }
}