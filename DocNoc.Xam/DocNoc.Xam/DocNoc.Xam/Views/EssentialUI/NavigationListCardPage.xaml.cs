using DocNoc.Xam.DataService;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.EssentialUI
{
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