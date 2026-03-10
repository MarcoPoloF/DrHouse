using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Alergias (dn-38-3).
    /// </summary>
    public class AlergiasViewModel : DocNocViewModel
    {
        #region Constructor

        public AlergiasViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarAlergia);

            ValidarTipoUsuario();

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

        public List<UsuarioAlergia> Alergias
        {
            get { return this.alergias; }
            set { SetProperty(ref alergias, value); }
        }
        private List<UsuarioAlergia> alergias;

        public string NuevaAlergia
        {
            get { return this.nuevaAlergia; }
            set { SetProperty(ref nuevaAlergia, value); }
        }
        private string nuevaAlergia;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AddCommand { get; set; }

        #endregion

        #region Methods

        private async void InsertarAlergia()
        {
            if (string.IsNullOrWhiteSpace(NuevaAlergia))
            {
                MensajeError("Introduzca un registro válido.");
                return;
            }

            var alergia = new UsuarioAlergia()
            {
                IdUsuario = IdExpediente,
                Alergia = NuevaAlergia
            };

            var resultadoApi = await DocNocApi.Usuarios.InsertaUsuarioAlergia(alergia);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            NuevaAlergia = string.Empty;

            CargarAlergias();
        }

        private async void CargarAlergias()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioAlergia(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (resultadoApi.Error)
            {
                return;
            }

            Alergias = new List<UsuarioAlergia>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarAlergias();
        }

        #endregion
    }
}

