using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

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

        private void SfSwitch_StateChanged(object sender, Syncfusion.Maui.Buttons.SwitchStateChangedEventArgs e)
        {

        }
    }
}