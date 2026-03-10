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
    /// Definición de ViewModel: Expediente (dn-36-3).
    /// </summary>
    public class ExpedienteViewModel : DocNocViewModel
    {
        #region Constructor

        public ExpedienteViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.DatosPersonalesCommand = new Command(IrDatosPersonales);
            this.AlergiasCommand = new Command(IrAlergias);
            this.TipoSangreCommand = new Command(IrTipoSangre);
            this.PadecimientosActualesCommand = new Command(IrPadecimientosActuales);
            this.AntecedentesFamiliaresCommand = new Command(IrAntecedentesFamiliares);
            this.HistoriaClinicaCommand = new Command(IrHistoriaClinica);
            this.HistorialConsultasCommand = new Command(IrHistorialConsultas);
            this.EstudiosAnalisisCommand = new Command(IrEstudiosAnalisis);
            this.MedicamentosCommand = new Command(IrMedicamentos);
            this.EliminarExpedienteCommand = new Command(EliminarExpediente);

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

        public Usuario Usuario
        {
            get { return this.usuario; }
            set { SetProperty(ref usuario, value); }
        }
        private Usuario usuario;

        public string NombreExpediente
        {
            get { return this.nombreExpediente; }
            set { SetProperty(ref nombreExpediente, value); }
        }
        private string nombreExpediente;

        public string TextoConfirmacion
        {
            get { return this.textoConfirmacion; }
            set { SetProperty(ref textoConfirmacion, value); }
        }
        private string textoConfirmacion;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command DatosPersonalesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AlergiasCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command TipoSangreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command PadecimientosActualesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command AntecedentesFamiliaresCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command HistoriaClinicaCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command HistorialConsultasCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command EstudiosAnalisisCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command MedicamentosCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command EliminarExpedienteCommand { get; set; }

        #endregion

        #region Methods

        private async void EliminarExpediente()
        {
            if(!EsUsuarioAdicional)
            {
                MensajeError("No se puede eliminar el expediente principal.");
                return;
            }

            if (NombreExpediente != Usuario.NombreCompleto)
            {
                MensajeError("El nombre del expediente no es el correcto.");
                return;
            }

            string idExpediente = Preferences.Get("idexpediente");

            var resultadoApi = await DocNocApi.Usuarios.EliminaUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = idExpediente });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Navigation.NavigateBack();
        }

        private void IrEstudiosAnalisis()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.EstudiosAnalisisViewModel), string.Empty, string.Empty);
        }

        private void IrAlergias()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.AlergiasViewModel), string.Empty, string.Empty);
        }

        private void IrPadecimientosActuales()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.PadecimientosActualesViewModel), string.Empty, string.Empty);
        }

        private void IrDatosPersonales()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Usuarios.DatosPersonalesViewModel), string.Empty, string.Empty);
        }

        private void IrAntecedentesFamiliares()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.AntecedentesFamiliaresViewModel), string.Empty, string.Empty);
        }

        private void IrTipoSangre()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.TipoSangreViewModel), string.Empty, string.Empty);
        }

        private void IrMedicamentos()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.MedicamentosViewModel), string.Empty, string.Empty);
        }

        private void IrHistoriaClinica()
        {
            if (EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.HistoriaClinicaViewModel), string.Empty, string.Empty);
        }

        private void IrHistorialConsultas()
        {
            if(EsUsuarioAdicional)
                Preferences.Set("TipoUsuario", "Adicional");

            Navigation.NavigateTo(typeof(ViewModels.Consultas.HistorialConsultasViewModel), string.Empty, string.Empty);
        }

        private async void CargarPerfil()
        {
            IsBusy = true;

            if (!EsUsuarioAdicional)
            {
                var respuestaApi = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = idUsuario });

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    IsBusy = false;
                    return;
                }

                Usuario = respuestaApi.Contenido;
            }
            else
            {
                string idUsuarioAdicional = Preferences.Get("idexpediente");

                var respuestaApi = await DocNocApi.Usuarios.TraeUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = idUsuarioAdicional });

                if (respuestaApi.Error)
                {
                    ErrorEntidad(respuestaApi);
                    IsBusy = false;
                    return;
                }

                Usuario = new Usuario(respuestaApi.Contenido);
            }

            if (Usuario.Error)
            {
                ErrorEntidad(Usuario);
                IsBusy = false;
                return;
            }

            TextoConfirmacion = $"Esta acción no se puede deshacer. Para confirmar ingrese el nombre del expediente ({Usuario.NombreCompleto}).";

            IsBusy = false;
        }

        public void OnAppearing()
        {
            CargarPerfil();
        }

        #endregion
    }
}



