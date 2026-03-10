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

namespace DocNoc.Xam.ViewModels.Expedientes
{
    /// <summary>
    /// Definición de ViewModel: Padecimientos Actuales (dn-41-3).
    /// </summary>
    public class PadecimientosActualesViewModel : DocNocViewModel
    {
        #region Constructor

        public PadecimientosActualesViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarPadecimiento);

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

        public List<UsuarioPadecimiento> Padecimientos
        {
            get { return this.padecimientos; }
            set { SetProperty(ref padecimientos, value); }
        }
        private List<UsuarioPadecimiento> padecimientos;

        public string NuevoPadecimiento
        {
            get { return this.nuevoPadecimiento; }
            set { SetProperty(ref nuevoPadecimiento, value); }
        }
        private string nuevoPadecimiento;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AddCommand { get; set; }

        #endregion

        #region Methods

        private async void InsertarPadecimiento()
        {
            if (string.IsNullOrWhiteSpace(NuevoPadecimiento))
            {
                MensajeError("Introduzca un registro válido.");
                return;
            }

            var registro = new UsuarioPadecimiento()
            {
                IdUsuario = IdExpediente,
                Padecimiento = NuevoPadecimiento
            };

            var resultadoApi = await DocNocApi.Usuarios.InsertaUsuarioPadecimiento(registro);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            NuevoPadecimiento = string.Empty;

            CargarPadecimientos();
        }

        private async void CargarPadecimientos()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioPadecimiento(new ParaFiltroUsuario() { IdUsuario = IdExpediente });

            if (resultadoApi.Error)
            {
                return;
            }

            Padecimientos = new List<UsuarioPadecimiento>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarPadecimientos();
        }

        #endregion
    }
}
