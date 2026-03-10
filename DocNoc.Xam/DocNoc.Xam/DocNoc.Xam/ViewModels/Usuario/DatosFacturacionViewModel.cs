using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Usuarios
{
    /// <summary>
    /// Definición de ViewModel: Datos de Facturación (dn-60-3).
    /// </summary>
    public class DatosFacturacionViewModel : DocNocViewModel
    {
        #region Constructor

        public DatosFacturacionViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.PopupCommand = new Command<SfPopup>(AbrirPopup);
            this.CancelCommand = new Command(CargarDatos);
            this.UpdateCommand = new Command(ActualizarUsuario);
            this.CheckChangesCommand = new Command(ValidarCambios);

        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private Usuario usuarioSinCambios;

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

        public Usuario Usuario
        {
            get { return this.usuario; }
            set { SetProperty(ref usuario, value); }
        }
        private Usuario usuario;

        public bool HayCambios
        {
            get { return this.hayCambios; }
            set { SetProperty(ref hayCambios, value); }
        }
        private bool hayCambios;

        #endregion

        #region Commands

        public Command CancelCommand { get; set; }

        public Command UpdateCommand { get; set; }

        public Command CheckChangesCommand { get; set; }

        #endregion

        #region Methods

        private async void ActualizarUsuario()
        {
            IsBusy = true;

            var respuestaApi = await DocNocApi.Usuarios.ActualizaPerfil(this.Usuario);

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
                IsBusy = false;
                return;
            }

            IsBusy = false;

            CargarDatos();
        }

        private async void CargarDatos()
        {
            IsBusy = true;

            var respuestaApi = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
                IsBusy = false;
                return;
            }

            Usuario = respuestaApi.Contenido;

            if (Usuario.Error)
            {
                ErrorEntidad(Usuario);
                IsBusy = false;
                return;
            }

            usuarioSinCambios = new Usuario()
            {
                RFC = respuestaApi.Contenido.RFC,
                RFCRazonSocial = respuestaApi.Contenido.RFCRazonSocial,
                RFCDireccion = respuestaApi.Contenido.RFCDireccion,
                RFCEstado = respuestaApi.Contenido.RFCEstado,
                RFCMunicipio = respuestaApi.Contenido.RFCMunicipio,
                RFCCP = respuestaApi.Contenido.RFCCP,
                RFCEmail = respuestaApi.Contenido.RFCEmail
            };

            HayCambios = false;

            IsBusy = false;
        }

        private void ValidarCambios()
        {
            if (Usuario == null)
                return;

            if (usuarioSinCambios == null)
                return;

            if (Usuario != usuarioSinCambios)
                HayCambios = true;
            else
                HayCambios = false;
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}
