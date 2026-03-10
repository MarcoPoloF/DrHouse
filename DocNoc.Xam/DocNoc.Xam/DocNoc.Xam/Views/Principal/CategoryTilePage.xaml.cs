using DocNoc.Xam.DataService;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Principal
{
    /// <summary>
    /// The Category Tile page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryTilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryTilePage" /> class.
        /// </summary>
        public CategoryTilePage()
        {
            InitializeComponent();
            this.BindingContext = CategoryDataService.Instance.CategoryPageViewModel;
        }
    }
}