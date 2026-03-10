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
    /// Definición de View (C#): Datos de Facturación (dn-60-3).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosFacturacionView : ContentPage
    {
        public DatosFacturacionView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Usuarios.DatosFacturacionViewModel;

            viewModel.OnAppearing();
        }
    }
}