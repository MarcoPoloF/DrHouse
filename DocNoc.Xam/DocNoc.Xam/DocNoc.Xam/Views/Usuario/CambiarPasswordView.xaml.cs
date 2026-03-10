using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CambiarPasswordView : ContentPage
    {
        public CambiarPasswordView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Usuarios.CambiarPasswordViewModel;

            viewModel.OnAppearing();
        }
    }
}