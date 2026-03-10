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
    public partial class AlergiasView : ContentPage
    {
        public AlergiasView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Expedientes.AlergiasViewModel;

            viewModel.OnAppearing();
        }
    }
}