using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Medicos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiciosMedicoView : ContentPage
    {
        public ServiciosMedicoView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Medicos.ServiciosMedicoViewModel;

            viewModel.OnAppearing();
        }
    }
}