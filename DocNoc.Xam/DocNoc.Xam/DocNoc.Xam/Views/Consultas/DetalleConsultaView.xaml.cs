using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Consultas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleConsultaView : ContentPage
    {
        public DetalleConsultaView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Consultas.DetalleConsultaViewModel;

            viewModel.OnAppearing();
        }
    }
}