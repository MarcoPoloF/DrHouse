using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Principal
{
    /// <summary>
    /// Definición de ViewModel: Home (dn-07-3).
    /// </summary>
    public class HomePageViewModel : DocNocViewModel
    {
        #region Fields

        //
        private List<CitasProximas> citas;

        private LoginTxt pageText;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public HomePageViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<LoginTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            //Se registran los comandos de la página.
            //this.LoginCommand = new Command(this.LoginClicked);
            //this.SignUpCommand = new Command(this.SignUpClicked);
            //this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);

            this.MisChatsCommand = new Command(MisChats);
            this.MisNotificacionesCommand = new Command(MisNotificaciones);
            this.RefreshCommand = new Command(CargarDatos);
            this.CitaSeleccionadaCommand = new Command(NavegarDetalleCita);
            this.ExpedienteSeleccionadoCommand = new Command(NavegarExpediente);
            this.MedicamentoSeleccionadoCommand = new Command(RegistrarTomaMedicamento);

            //BadgeChats = ProcesarEstatusBadge(false);
            //BadgeNotificaciones = ProcesarEstatusBadge(false);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when the appointments list is refreshed.
        /// </summary>
        public Command RefreshCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when an appointment is tapped.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

        public Command CitaSeleccionadaCommand { get; set; }

        public Command ExpedienteSeleccionadoCommand { get; set; }

        public Command MedicamentoSeleccionadoCommand { get; set; }

        #endregion

        #region Flags

        public string TieneMensajes
        {
            get { return this.tieneMensajes; }
            set { SetProperty(ref tieneMensajes, value); }
        }
        private string tieneMensajes;

        public bool HayCitas
        {
            get { return this.hayCitas; }
            set { SetProperty(ref hayCitas, value); }
        }
        private bool hayCitas;

        public bool HayExpedientes
        {
            get { return this.hayExpedientes; }
            set { SetProperty(ref hayExpedientes, value); }
        }
        private bool hayExpedientes;

        public bool HayMedicamentos
        {
            get { return this.hayMedicamentos; }
            set { SetProperty(ref hayMedicamentos, value); }
        }
        private bool hayMedicamentos;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the page text.
        /// </summary>
        public LoginTxt PageText
        {
            get { return this.pageText; }
            set { SetProperty(ref pageText, value); }
        }

        public List<CitasProximas> Citas
        {
            get { return this.citas; }
            set { SetProperty(ref citas, value); }
        }

        public List<ResultadoUsuarioAdicional> Expedientes
        {
            get { return this.expedientes; }
            set { SetProperty(ref expedientes, value); }
        }
        private List<ResultadoUsuarioAdicional> expedientes;

        public List<MiMedicamento> Medicamentos
        {
            get { return this._medicamentos; }
            set { SetProperty(ref _medicamentos, value); }
        }
        private List<MiMedicamento> _medicamentos;

        public List<Anuncio> Anuncios
        {
            get { return this.anuncios; }
            set { SetProperty(ref anuncios, value); }
        }
        private List<Anuncio> anuncios;

        #endregion

        #region Methods

        private async void CargarCitas()
        {
            IsBusy = true;

            Citas = new List<CitasProximas>();
            HayCitas = false;

            var respuestaApi = await DocNocApi.Citas.CitaProxima(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                MensajeError($"No se pudieron cargar las citas: {respuestaApi.CadenaErrores()}");
                return;
            }

            Citas = new List<CitasProximas>(respuestaApi.Registros);

            if (Citas.Count > 0)
                HayCitas = true;

            IsBusy = false;
        }

        private void CargarDatos()
        {
            TieneMensajes = "Light";
            CargarCitas();
            CargarExpedientes();
            CargarAnuncios();
            CargarEstatusChats();
            CargarEstatusNotificaciones();
        }

        private async void CargarAnuncios()
        {
            if (Anuncios != null && Anuncios.Count > 0)
                return;

            var resultadoApi = await DocNocApi.Usuarios.ListadoAnunciosPacientes();

            if (resultadoApi.Error)
            {
                MensajeError($"No se pudieron cargar los anuncios: {resultadoApi.CadenaErrores()}");
                return;
            }

            Anuncios = new List<Anuncio>(resultadoApi.Registros);
        }

        private async void CargarExpedientes()
        {
            Expedientes = new List<ResultadoUsuarioAdicional>();
            HayExpedientes = false;

            var resultadoApi = await DocNocApi.Citas.TraeUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (resultadoApi.Error)
            {
                MensajeError($"No se pudieron cargar los expedientes: {resultadoApi.CadenaErrores()}");
                return;
            }

            var listaPacientes = new List<ResultadoUsuarioAdicional>();
            var respuestaApi1 = await DocNocApi.Usuarios.TraeUsuario(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi1.Error)
            {
                MensajeError($"No se pudieron cargar los usuarios: {resultadoApi.CadenaErrores()}");
                return;
            }

            listaPacientes.Add(new ResultadoUsuarioAdicional() { IdUsuarioAdicional = string.Empty, NombreAdicional = respuestaApi1.Contenido.NombreCompleto });

            listaPacientes.AddRange(resultadoApi.Registros);

            Expedientes = new List<ResultadoUsuarioAdicional>(listaPacientes);

            if (Expedientes.Count > 0)
            {
                HayExpedientes = true;
                CargarMedicamentos();
            }
        }

        private async void CargarMedicamentos()
        {
            Medicamentos = new List<MiMedicamento>();
            HayMedicamentos = false;

            var medicamentos = new List<MiMedicamento>();

            foreach (var expediente in this.expedientes)
            {
                string idPaciente = expediente.IdUsuarioAdicional == string.Empty ? idUsuario : expediente.IdUsuarioAdicional;

                var resultadoApi = await DocNocApi.Pacientes.MisMedicamentos(new ParaFiltroUsuario() { IdUsuario = idPaciente });

                if (resultadoApi.Error)
                {
                    MensajeError($"No se pudieron cargar los medicamentos del paciente '{expediente.NombreAdicional}': {resultadoApi.CadenaErrores()}");
                    continue;
                }

                foreach(var medicamento in resultadoApi.Registros)
                {
                    medicamento.IdPaciente = idPaciente;
                    medicamento.Paciente = expediente.IdUsuarioAdicional == string.Empty ? "Mí" : expediente.NombreAdicional;
                    medicamento.EsPacienteAdicional = expediente.IdUsuarioAdicional != string.Empty ? true : false;
                }

                medicamentos.AddRange(resultadoApi.Registros);
            }

            Medicamentos = new List<MiMedicamento>(medicamentos.OrderBy(o => o.FechaToma));

            if (Medicamentos.Count > 0)
                HayMedicamentos = true;
        }

        private void NavegarDetalleCita(object obj)
        {
            var cita = (CitasProximas)obj;

            Preferences.Set("IdCita", cita.IdCita.ToString());

            Navigation.NavigateTo(typeof(ViewModels.Citas.DetalleCitaViewModel), string.Empty, string.Empty);
        }

        private void NavegarExpediente(object obj)
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

        private async void RegistrarTomaMedicamento(object obj)
        {
            var medicamento = (MiMedicamento)obj;

            medicamento.Tomado = true;

            //await Task.Delay(1000);

            var tomaMedicamento = new TomaMedicamento()
            {
                IdUsuario = idUsuario,
                IdMedicamento = medicamento.IdMedicamento,
                FechaToma = medicamento.FechaToma
            };

            if (medicamento.EsPacienteAdicional)
                tomaMedicamento.IdUsuarioAdicional = medicamento.IdPaciente;                

            var resultadoApi = await DocNocApi.Pacientes.ActualizaMedicamentoATomado(tomaMedicamento);

            if (resultadoApi.Error)
                ErrorEntidad(resultadoApi);

            CargarMedicamentos();
        }

        public void OnAppearing()
        {
            BadgeChats = ProcesarEstatusBadge(false);
            BadgeNotificaciones = ProcesarEstatusBadge(false);
            CargarDatos();
        }

        #endregion
    }
}
