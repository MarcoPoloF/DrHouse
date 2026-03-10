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
    public partial class ExpedienteView : ContentPage
    {
        public ExpedienteView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Expedientes.ExpedienteViewModel;

            viewModel.OnAppearing();
        }
    }
}