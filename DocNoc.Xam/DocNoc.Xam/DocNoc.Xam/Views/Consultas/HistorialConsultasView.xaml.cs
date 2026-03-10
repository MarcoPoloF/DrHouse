using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Consultas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistorialConsultasView : ContentPage
    {
        public HistorialConsultasView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Consultas.HistorialConsultasViewModel;

            viewModel.OnAppearing();
        }
    }
}