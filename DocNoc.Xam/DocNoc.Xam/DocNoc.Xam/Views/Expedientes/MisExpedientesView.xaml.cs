using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Expedientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisExpedientesView : ContentPage
    {
        public MisExpedientesView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Expedientes.MisExpedientesViewModel;

            viewModel.OnAppearing();
        }
    }
}