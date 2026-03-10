using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Suscripcion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiSuscripcionView : ContentPage
    {
        public MiSuscripcionView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Suscripcion.MiSuscripcionViewModel;

            viewModel.OnAppearing();
        }
    }
}