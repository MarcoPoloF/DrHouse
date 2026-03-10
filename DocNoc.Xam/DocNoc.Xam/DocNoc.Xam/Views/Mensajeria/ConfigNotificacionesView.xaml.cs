using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Mensajeria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigNotificacionesView : ContentPage
    {
        public ConfigNotificacionesView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Mensajeria.ConfigNotificacionesViewModel;

            viewModel.OnAppearing();
        }

        private void SfSwitch_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {

        }
    }
}