using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Medicos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaMedicosView : ContentPage
    {
        public ListaMedicosView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = this.BindingContext as ViewModels.Medicos.ListaMedicosViewModel;

            vm.OnAppearing();
        }
    }
}