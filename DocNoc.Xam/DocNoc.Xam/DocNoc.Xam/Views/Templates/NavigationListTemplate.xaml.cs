using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Templates
{
    /// <summary>
    /// Navigation list template.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationListTemplate : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationListTemplate"/> class.
        /// </summary>
		public NavigationListTemplate()
        {
            InitializeComponent();
        }
    }
}