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
    /// Definición de ViewModel: Mis Expedientes (dn-34-3).
    /// </summary>
    public class MisExpedientesViewModel : DocNocViewModel
    {
        #region Constructor

        public MisExpedientesViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AgregarPopupCommand = new Command(AgregarRegistro);
            this.AddCommand = new Command(NuevoExpediente);
            this.DetailCommand = new Command<object>(IrExpediente);
            this.IrSuscripcionCommand = new Command(AbrirSuscripcion);
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

        public List<ResultadoUsuarioAdicional> Pacientes
        {
            get { return this.pacientes; }
            set { SetProperty(ref pacientes, value); }
        }
        private List<ResultadoUsuarioAdicional> pacientes;

        public string NombreExpediente
        {
            get { return this.nombreExpediente; }
            set { SetProperty(ref nombreExpediente, value); }
        }
        private string nombreExpediente;

        public bool AgregarVisible
        {
            get { return this.agregarVisible; }
            set { SetProperty(ref agregarVisible, value); }
        }
        private bool agregarVisible;

        #endregion

        #region Commands

        public Command AddCommand { get; set; }

        public Command DetailCommand { get; set; }

        public Command AgregarPopupCommand { get; set; }

        #endregion

        #region Methods

        private void IrExpediente(object obj)
        {
            var expediente = (ResultadoUsuarioAdicional)obj;

            if (expediente.IdUsuarioAdicional != string.Empty)
            {
                Preferences.Set("idexpediente", expediente.IdUsuarioAdicional);
                Preferences.Set("TipoUsuario", "Adicional");
            }
            else
            {
                Preferences.Set("idexpediente", idUsuario);
            }

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.ExpedienteViewModel), string.Empty, string.Empty);
        }

        private async void AgregarRegistro()
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            ParaFiltroUsuarioyDato filtro = new ParaFiltroUsuarioyDato()
            {
                IdUsuario = idUsuario,
                Dato = "CrearNuevoExpediente"
            };

            var resultadoApi = await DocNocApi.OpenPay.PreValidacionPaciente(filtro);

            if (resultadoApi.Error)
            {
                var errorArray = resultadoApi.Mensajes[0].Contenido.Split('|');

                if (errorArray.Length == 3)
                {
                    ErrorSuscripcion = errorArray[1];
                    EnlaceSuscripcion = errorArray[2];
                    ErrorSuscripcionVisible = true;
                    return;
                }

                ErrorEntidad(resultadoApi);

                return;
            }

            AgregarVisible = true;
        }

        private async void NuevoExpediente()
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(NombreExpediente))
            {
                MensajeError("Introduzca un nombre para el expediente.");
                return;
            }

            var expediente = new UsuarioAdicional()
            {
                IdUsuario = idUsuario,
                Nombre = NombreExpediente
            };

            var respuestaApi = await DocNocApi.Usuarios.AgregaUsuarioAdicional(expediente);

            if (respuestaApi.Error)
            {
                var errorArray = respuestaApi.Mensajes[0].Contenido.Split('|');

                if (errorArray.Length == 3)
                {
                    ErrorSuscripcion = errorArray[1];
                    EnlaceSuscripcion = errorArray[2];
                    ErrorSuscripcionVisible = true;
                    return;
                }

                ErrorEntidad(respuestaApi);

                return;
            }

            NombreExpediente = null;

            CargarPacientes();
        }

        private async void CargarPacientes()
        {
            var resultadoApi = await DocNocApi.Citas.TraeUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            var listaPacientes = new List<ResultadoUsuarioAdicional>();

            listaPacientes.Add(new ResultadoUsuarioAdicional() { IdUsuarioAdicional = string.Empty, NombreAdicional = "Mi perfil" });

            if (!resultadoApi.Error)
            {
                foreach (var paciente in resultadoApi.Registros)
                {
                    listaPacientes.Add(paciente);
                }

                Pacientes = new List<ResultadoUsuarioAdicional>(listaPacientes);
            }
        }

        public void OnAppearing()
        {
            CargarPacientes();
        }

        #endregion
    }
}


