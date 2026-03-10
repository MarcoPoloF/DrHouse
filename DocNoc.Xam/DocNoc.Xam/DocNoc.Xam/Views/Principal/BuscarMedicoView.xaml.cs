using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Principal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscarMedicoView : ContentPage
    {
        public BuscarMedicoView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Principal.BuscarMedicoViewModel;

            viewModel.OnAppearing();
        }
    }
}