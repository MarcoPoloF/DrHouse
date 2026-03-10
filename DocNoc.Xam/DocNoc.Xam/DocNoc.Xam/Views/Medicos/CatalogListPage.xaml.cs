using DocNoc.Xam.DataService;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Medicos
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogListPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogListPage" /> class.
        /// </summary>
        public CatalogListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = this.BindingContext as ViewModels.Medicos.CatalogPageViewModel;

            vm.OnAppearing();
        }
    }
}