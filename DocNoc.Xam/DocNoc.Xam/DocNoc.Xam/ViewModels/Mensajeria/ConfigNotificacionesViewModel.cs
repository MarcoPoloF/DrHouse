using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Buttons;
using Syncfusion.Maui.Chat;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Mensajeria
{
    /// <summary>
    /// Definición de ViewModel: Configuración de Notificaciones (dn-68-3).
    /// </summary>
    public class ConfigNotificacionesViewModel : DocNocViewModel
    {
        #region Constructor

        public ConfigNotificacionesViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-68-3).
            //PageText = text.Get<PaginaTxt>("dn-68-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            this.BackCommand = new Command(Regresar);
            this.CambiaMensajesCommand = new Command (CambiaMensajes);
            this.CambiaMedicamentosCommand = new Command(CambiaMedicamentos);
            this.CambiaDocnocCommand = new Command(CambiaDocnoc);
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

        public bool MensajesActivo
        {
            get { return this.mensajesActivo; }
            set { SetProperty(ref mensajesActivo, value); }
        }
        private bool mensajesActivo;

        public bool MedicamentosActivo
        {
            get { return this.medicamentosActivo; }
            set { SetProperty(ref medicamentosActivo, value); }
        }
        private bool medicamentosActivo;

        public bool DocnocActivo
        {
            get { return this.docnocActivo; }
            set { SetProperty(ref docnocActivo, value); }
        }
        private bool docnocActivo;

        #endregion

        #region Commands
        public Command CambiaMensajesCommand { get; set; }
        public Command CambiaMedicamentosCommand { get; set; }
        public Command CambiaDocnocCommand { get; set; }
        #endregion

        #region Methods

        private async void CargarDatos()
        {
            var respuestaApi = await DocNocApi.Usuarios.TraeUsuarioSetNotificacion(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                ErrorEntidad(respuestaApi);
                return;
            }
            else
            {
                foreach (var item in respuestaApi.Registros)
                {
                    switch (item.IdParametroNotificacion)
                    {
                        case 8:
                            MensajesActivo = item.AppWeb;
                            break;
                        case 9:
                            MedicamentosActivo = item.AppWeb;
                            break;
                        case 10:
                            DocnocActivo = item.AppWeb;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private async void CambiaMensajes()
        {
            await DocNocApi.Usuarios.ModificaUsuarioSetNotificacionAppWeb(new UsuarioSetNotificacion()
            {
                IdUsuario = idUsuario,
                IdParametroNotificacion = 8,
                AppWeb = MensajesActivo
            });
            CargarDatos();
        }

        private async void CambiaMedicamentos()
        {
            await DocNocApi.Usuarios.ModificaUsuarioSetNotificacionAppWeb(new UsuarioSetNotificacion()
            {
                IdUsuario = idUsuario,
                IdParametroNotificacion = 9,
                AppWeb = MedicamentosActivo
            });
            CargarDatos();
        }
        private async void CambiaDocnoc()
        {
            await DocNocApi.Usuarios.ModificaUsuarioSetNotificacionAppWeb(new UsuarioSetNotificacion()
            {
                IdUsuario = idUsuario,
                IdParametroNotificacion = 10,
                AppWeb = DocnocActivo
            });
            CargarDatos();
        }


        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}


