using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.UIKit
{
    /// <summary>
    /// Page to show chat profile page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatProfilePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatProfilePage" /> class.
        /// </summary>
        public ChatProfilePage()
        {
            InitializeComponent();
            this.ProfileImage.Source = App.BaseImageUrl + "ProfileImage11.png";
        }
    }
}