using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Bienvenida;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Acceso;
using DocNoc.Xam.Views.Bienvenida;
using Syncfusion.Maui.Rotator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Bienvenida
{
    /// <summary>
    /// Definición de ViewModel: Bienvenida (dn-01-3, dn-02-3, dn-03-3).
    /// </summary>
    public class OnBoardingAnimationViewModel : DocNocViewModel
    {
        #region Fields

        private List<PanelBienvenida> paneles;

        private int selectedIndex;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel(INavigationService nav, ITextService text, IPreferenceService pref)
        {
            Navigation = nav;

            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);

            //Carga de textos: Bienvenida 1 (dn-01-3).
            var texto1 = text.Get<BienvenidaTxt>("dn-01-3", pref);
            //Carga de textos: Bienvenida 2 (dn-02-3).
            var texto2 = text.Get<BienvenidaTxt>("dn-02-3", pref);
            //Carga de textos: Bienvenida 3 (dn-03-3).
            var texto3 = text.Get<BienvenidaTxt>("dn-03-3", pref);

            SelectedIndex = 0;

            Paneles = new List<PanelBienvenida>()
            {
                new PanelBienvenida() { Imagen = "OnBoarding1.png" },
                //Se invierten slides 2 y 3 a petición del cliente.
                new PanelBienvenida() { Imagen = "OnBoarding3.png" },
                new PanelBienvenida() { Imagen = "OnBoarding2.png" },
                new PanelBienvenida() { Imagen = "OnBoarding4.png" }
            };
        }

        #endregion

        #region Properties

        public List<PanelBienvenida> Paneles
        {
            get { return this.paneles; }
            set { SetProperty(ref paneles, value); }
        }

        public int SelectedIndex
        {
            get { return this.selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public ICommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public ICommand NextCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            this.MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            if (SelectedIndex == 0)
            {
                SelectedIndex = 1;
            }
            else
            {
                if (SelectedIndex == 1)
                {
                    SelectedIndex = 2;
                }
                else
                {
                    this.MoveToNextPage();
                }
            }
        }

        private void MoveToNextPage()
        {
            //Navegación a página "Login" (dn-04-3).
            Navigation.NavigateTo(typeof(ViewModels.Acceso.LoginPageViewModel), string.Empty, string.Empty, true);
        }

        #endregion
    }
}
