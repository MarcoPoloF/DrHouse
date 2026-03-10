using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DocNoc.Xam.Views.Principal
{
    /// <summary>
    /// Definición de View (C#): Mi Perfil (dn-59-3).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiPerfilView : ContentPage
    {
        public MiPerfilView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Principal.MiPerfilViewModel;

            viewModel.OnAppearing();
        }
    }
}