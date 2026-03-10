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
    /// Definición de ViewModel: Mi Perfil (dn-59-3).
    /// </summary>
    public class MiPerfilViewModel : DocNocViewModel
    {
        #region Constructor

        public MiPerfilViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.PopupCommand = new Command<SfPopupLayout>(AbrirPopup);
            this.DatosPersonalesCommand = new Command(IrDatosPersonales);
            this.CambiarPasswordCommand = new Command(IrCambiarPassword);
            this.MisExpedientesCommand = new Command(IrMisExpedientes);
            this.DatosFacturacionCommand = new Command(IrDatosFacturacion);
            this.FormasPagoCommand = new Command(IrFormasPago);
            this.SuscripcionCommand = new Command(IrSuscripcion);
            this.TerminosCondicionesCommand = new Command(IrTerminosCondiciones);
            this.AvisoPrivacidadCommand = new Command(IrAvisoPrivacidad);
            this.PreguntasFrecuentesCommand = new Command(IrPreguntasFrecuentes);
            this.AjustesNotificacionesCommand = new Command(IrAjustesNotificaciones);
            this.AyudaCommand = new Command(IrAyuda);
            this.AbrirChatAyudaCommand = new Command(AbrirChatAyuda);
            this.CerrarSesionCommand = new Command(CerrarSesion);
            this.AcercaDeCommand = new Command(IrAcercaDe);
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the page text.
        /// </summary>
        //public PaginaTxt PageText
        //{
        //    get { return this.pageText; }
        //    set { SetProperty(ref pageText, value); }
        //}

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command DatosPersonalesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command CambiarPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command MisExpedientesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command DatosFacturacionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command FormasPagoCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command SuscripcionCommand { get; set; }

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

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AjustesNotificacionesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AyudaCommand { get; set; }

        public Command AbrirChatAyudaCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command CerrarSesionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AcercaDeCommand { get; set; }

        #endregion

        #region Methods
        
        private void IrAcercaDe()
        {
            Navigation.NavigateTo(typeof(ViewModels.Principal.AcercaDeViewModel), string.Empty, string.Empty);
        }

        private void IrAjustesNotificaciones()
        {
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.ConfigNotificacionesViewModel), string.Empty, string.Empty);
        }

        private void IrAvisoPrivacidad()
        {
            AbrirUri(@"https://docnoc.mx/apriv.html", "Aviso de Privacidad");
        }

        private void IrAyuda()
        {
            //Navigation.NavigateTo(typeof(ViewModels.Acceso.LoginPageViewModel), string.Empty, string.Empty);
        }

        private void IrCambiarPassword()
        {
            Navigation.NavigateTo(typeof(ViewModels.Usuarios.CambiarPasswordViewModel), string.Empty, string.Empty);
        }

        private void IrDatosFacturacion()
        {
            Navigation.NavigateTo(typeof(ViewModels.Usuarios.DatosFacturacionViewModel), string.Empty, string.Empty);
        }

        private void IrDatosPersonales()
        {
            Navigation.NavigateTo(typeof(ViewModels.Usuarios.DatosPersonalesViewModel), string.Empty, string.Empty);
        }

        private void IrFormasPago()
        {
            Navigation.NavigateTo(typeof(ViewModels.Suscripcion.FormasPagoViewModel), string.Empty, string.Empty);
        }

        private void IrMisExpedientes()
        {
            Navigation.NavigateTo(typeof(ViewModels.Expedientes.MisExpedientesViewModel), string.Empty, string.Empty);
        }

        private void IrPreguntasFrecuentes()
        {
            AbrirUri(@"https://docnoc.mx/faqP.html", "Preguntas Frecuentes");
        }

        private void IrSuscripcion()
        {
            Navigation.NavigateTo(typeof(ViewModels.Suscripcion.MiSuscripcionViewModel), string.Empty, string.Empty);
        }

        private void IrTerminosCondiciones()
        {
            AbrirUri(@"https://docnoc.mx/tyc.html", "Términos y Condiciones");
        }

        private async void AbrirChatAyuda()
        {
            var respuestaApi = await DocNocApi.Usuarios.cW4UZsWHdz4bYWF2(new ParaFiltroUsuarioyDato() { Dato = "yjFLj*X#6oYsN@Kj" });

            Preferences.Set("IdUsuario_Chat", respuestaApi.Contenido.IdUsuario);
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.ChatViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {

        }

        #endregion
    }
}

