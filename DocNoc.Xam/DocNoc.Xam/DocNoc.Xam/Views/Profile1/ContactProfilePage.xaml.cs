using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Profile1
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactProfilePage
    {
        public ContactProfilePage()
        {
            InitializeComponent();
            this.ProfileImage.Source = App.BaseImageUrl + "ContactProfileImage.png";
        }
    }
}