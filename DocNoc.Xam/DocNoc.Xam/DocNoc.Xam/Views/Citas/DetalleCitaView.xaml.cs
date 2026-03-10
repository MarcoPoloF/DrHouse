using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Citas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleCitaView : ContentPage
    {
        public DetalleCitaView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = this.BindingContext as ViewModels.Citas.DetalleCitaViewModel;
            
            vm.OnAppearing();
        }
    }
}