using DocNoc.Xam.DataService;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Principal
{
    /// <summary>
    /// Page to show photo album
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumPage
    {
        public AlbumPage()
        {
            InitializeComponent();
            this.BindingContext = AlbumDataService.Instance.AlbumViewModel;
        }
    }
}