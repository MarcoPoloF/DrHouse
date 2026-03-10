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
    /// Definición de View (C#): Home (dn-07-3).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage" /> class.
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as ViewModels.Principal.HomePageViewModel;

            viewModel.OnAppearing();
        }
    }
}