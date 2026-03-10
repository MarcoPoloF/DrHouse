using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Principal
{
    /// <summary>
    /// Definición de ViewModel: Acerca De.
    /// </summary>
    public class AcercaDeViewModel : DocNocViewModel
    {
        #region Constructor

        public AcercaDeViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            this.BackCommand = new Command(Regresar);
            this.TerminosCondicionesCommand = new Command(IrTerminosCondiciones);
            this.AvisoPrivacidadCommand = new Command(IrAvisoPrivacidad);
            this.PreguntasFrecuentesCommand = new Command(IrPreguntasFrecuentes);

        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        #endregion

        #region Properties

        public bool WebViewVisible
        {
            get { return this._webViewVisible; }
            set { SetProperty(ref _webViewVisible, value); }
        }
        private bool _webViewVisible;

        public string WebViewTitulo
        {
            get { return this._webViewTitulo; }
            set { SetProperty(ref _webViewTitulo, value); }
        }
        private string _webViewTitulo;

        public string WebViewRuta
        {
            get { return this._webViewRuta; }
            set { SetProperty(ref _webViewRuta, value); }
        }
        private string _webViewRuta;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command TerminosCondicionesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AvisoPrivacidadCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command PreguntasFrecuentesCommand { get; set; }

        #endregion

        #region Methods

        private void IrAvisoPrivacidad()
        {
            AbrirWebView(@"https://docnoc.mx/apriv.html", "Aviso de Privacidad");
        }

        private void IrPreguntasFrecuentes()
        {
            AbrirWebView(@"https://docnoc.mx/faqP.html", "Preguntas Frecuentes");
        }

        private void IrTerminosCondiciones()
        {
            AbrirWebView(@"https://docnoc.mx/tyc.html", "Términos y Condiciones");
        }

        private void AbrirWebView(string ruta, string titulo)
        {
            WebViewRuta = ruta;
            WebViewTitulo = titulo;
            WebViewVisible = true;
        }

        public void OnAppearing()
        {

        }

        #endregion
    }
}


