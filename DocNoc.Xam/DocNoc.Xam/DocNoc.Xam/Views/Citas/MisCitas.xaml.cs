using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocNoc.Xam.Views.Citas
{
    /// <summary>
    /// Definición de View (C#): Mis Citas (dn-26-3).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MisCitas : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MisCitas" /> class.
        /// </summary>
        public MisCitas()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vm = this.BindingContext as ViewModels.Citas.MisCitasViewModel;

            vm.OnAppearing();
        }
    }
}