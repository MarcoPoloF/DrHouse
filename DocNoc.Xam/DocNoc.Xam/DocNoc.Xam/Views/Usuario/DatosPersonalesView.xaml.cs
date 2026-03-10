using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Usuarios
{
    /// <summary>
    /// Definición de View (C#): Datos Personales (dn-37-3).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosPersonalesView : ContentPage
    {
        public DatosPersonalesView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Usuarios.DatosPersonalesViewModel;

            viewModel.OnAppearing();
        }
    }
}