using Microsoft.Maui.Controls;
using DocNoc.Xam.Models.Citas;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using DocNoc.Models;
using System;
using DocNoc.Xam.Interfaces;
using Syncfusion.Maui.ProgressBar;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace DocNoc.Xam.ViewModels.Citas
{
    /// <summary>
    /// ViewModel for my address page.
    /// </summary>
    public class MyAddressViewModel : DocNocViewModel
    {
        #region Fields
        private List<DetalleFecha> dias;

        private List<TraeAgendasMedicoAPP> horas;

        private List<ResultadoUsuarioAdicional> pacientes;

        private DetalleFecha diaSeleccionado;

        private ResultadoUsuarioAdicional pacienteSeleccionado;

        private string pacienteExterno;

        private string motivoConsulta;

        private string pacienteCita;

        private string citaLetra;

        private bool seccion1Activa;
        private bool seccion2Activa;
        private bool seccion3Activa;

        private bool avanzarVisible;
        private bool retrocederVisible;
        private bool agendarVisible;

        private bool conConsultas;
        private bool sinConsultas;

        private StepStatus step1;
        private StepStatus step2;
        private StepStatus step3;

        #endregion

        #region Properties

        public string IdMedico
        {
            get { return this.idMedico; }
            set { SetProperty(ref idMedico, value); }
        }
        private string idMedico;

        public List<DetalleFecha> Dias 
        {
            get { return this.dias; }
            set { SetProperty(ref dias, value); }
        }

        public List<DisponibilidadConsultorio> Consultorios
        {
            get { return this.consultorios; }
            set { SetProperty(ref consultorios, value); }
        }
        private List<DisponibilidadConsultorio> consultorios;

        public List<TraeAgendasMedicoAPP> Horas
        {
            get { return this.horas; }
            set { SetProperty(ref horas, value); }
        }

        public List<ResultadoUsuarioAdicional> Pacientes
        {
            get { return this.pacientes; }
            set { SetProperty(ref pacientes, value); }
        }

        public DetalleFecha DiaSeleccionado
        {
            get { return this.diaSeleccionado; }
            set { SetProperty(ref diaSeleccionado, value); }
        }

        //public TraeAgendasMedicoAPP HoraSeleccionada
        //{
        //    get { return this.horaSeleccionada; }
        //    set { SetProperty(ref horaSeleccionada, value); }
        //}

        public CitaConsultorio HorarioSeleccionado
        {
            get { return this.horarioSeleccionado; }
            set { SetProperty(ref horarioSeleccionado, value); }
        }
        private CitaConsultorio horarioSeleccionado;

        public DisponibilidadConsultorio ConsultorioSeleccionado
        {
            get { return this._consultorioSeleccionado; }
            set { SetProperty(ref _consultorioSeleccionado, value); }
        }
        private DisponibilidadConsultorio _consultorioSeleccionado;

        public ResultadoUsuarioAdicional PacienteSeleccionado
        {
            get { return this.pacienteSeleccionado; }
            set 
            { 
                SetProperty(ref pacienteSeleccionado, value);  

                if(this.pacienteSeleccionado != null)
                {
                    PacienteExterno = string.Empty;
                }
            }
        }

        public string PacienteExterno
        {
            get { return this.pacienteExterno; }
            set 
            { 
                SetProperty(ref pacienteExterno, value);

                if (value.Length > 0)
                {
                    PacienteSeleccionado = null;
                    Pacientes = new List<ResultadoUsuarioAdicional>(Pacientes);
                }
            }
        }

        public string MotivoConsulta
        {
            get { return this.motivoConsulta; }
            set { SetProperty(ref motivoConsulta, value); }
        }

        public bool Seccion1Activa
        {
            get { return this.seccion1Activa; }
            set { SetProperty(ref seccion1Activa, value); }
        }

        public bool Seccion2Activa
        {
            get { return this.seccion2Activa; }
            set { SetProperty(ref seccion2Activa, value); }
        }

        public bool Seccion3Activa
        {
            get { return this.seccion3Activa; }
            set { SetProperty(ref seccion3Activa, value); }
        }

        public bool AvanzarVisible
        {
            get { return this.avanzarVisible; }
            set { SetProperty(ref avanzarVisible, value); }
        }

        public bool RetrocederVisible
        {
            get { return this.retrocederVisible; }
            set { SetProperty(ref retrocederVisible, value); }
        }

        public bool AgendarVisible
        {
            get { return this.agendarVisible; }
            set { SetProperty(ref agendarVisible, value); }
        }

        public bool ConConsultas
        {
            get { return this.conConsultas; }
            set { SetProperty(ref conConsultas, value); }
        }

        public bool SinConsultas
        {
            get { return this.sinConsultas; }
            set { SetProperty(ref sinConsultas, value); }
        }

        public bool GenerandoCita
        {
            get { return this.generandoCita; }
            set { SetProperty(ref generandoCita, value); }
        }
        private bool generandoCita;

        public string PacienteCita
        {
            get { return this.pacienteCita; }
            set { SetProperty(ref pacienteCita, value); }
        }

        public string CitaLetra
        {
            get { return this.citaLetra; }
            set { SetProperty(ref citaLetra, value); }
        }

        public StepStatus Step1
        {
            get { return this.step1; }
            set { SetProperty(ref step1, value); }
        }

        public StepStatus Step2
        {
            get { return this.step2; }
            set { SetProperty(ref step2, value); }
        }

        public StepStatus Step3
        {
            get { return this.step3; }
            set { SetProperty(ref step3, value); }
        }

        #endregion

        #region Constructor
        public MyAddressViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            CargarDias();
            CargarPacientes();

            IsBusy = false;
            GenerandoCita = false;

            Seccion1Activa = true;
            Seccion2Activa = false;
            Seccion3Activa = false;

            AvanzarVisible = true;
            RetrocederVisible = false;
            AgendarVisible = false;

            Step1 = StepStatus.InProgress;
            Step2 = StepStatus.NotStarted;
            Step3 = StepStatus.NotStarted;

            MotivoConsulta = string.Empty;

            this.BackCommand = new Command(this.Regresar);
            this.AddCardCommand = new Command(this.AddCardButtonClicked);
            this.NextCommand = new Command(this.Avanzar);
            this.BackwardsCommand = new Command(this.Retroceder);
            this.FinishCommand = new Command(this.AgendarCita);
            //this.HorarioSeleccionadoCommand = new Command<CitaConsultorio>(SeleccionarHorario);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the add card button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void AddCardButtonClicked(object obj)
        {
            var fecha = (DetalleFecha)obj;

            CargarConsultorios(fecha.Fecha);
        }

        private void CargarDias()
        {
            var dia = DateTime.Now.Date;

            var listaDias = new List<DetalleFecha>();

            listaDias.Add(new DetalleFecha(dia));

            for (int i = 0; i < 30; i++)
            {
                listaDias.Add(new DetalleFecha(dia.AddDays(i + 1)));
            }

            Dias = new List<DetalleFecha>(listaDias);

            DiaSeleccionado = Dias.First();

            CargarConsultorios(DiaSeleccionado.Fecha.Date);
        }

        private async void CargarConsultorios(DateTime fechaDia)
        {
            IsBusy = true;

            IdMedico = Preferences.Get("PerfilMedico_Id");

            var consultoriosApi = await DocNocApi.Consultorios.TraeMisConsultoriosParaAPP(new ParaFiltroUsuario() { IdUsuario = IdMedico });

            if (consultoriosApi.Error)
            {
                ErrorEntidad(consultoriosApi);
                IsBusy = false;
                return;
            }

            var listaConsultorios = new List<DisponibilidadConsultorio>(consultoriosApi.Registros);

            //Se cargan los horarios de cada consultorio.
            foreach(var consultorio in listaConsultorios)
            {
                var horariosApi = await DocNocApi.Citas.TraeAgendaMedicoAPP(new ParaFiltroUsuarioConsultorio() 
                    {
                        IdUsuario = IdMedico, 
                        IdConsultorio = consultorio.IdConsultorio, 
                        Fecha = fechaDia 
                    });

                if (horariosApi.Error)
                {
                    ErrorEntidad(horariosApi);
                    IsBusy = false;
                    return;
                }

                consultorio.CitasDisponibles = new List<CitaConsultorio>(horariosApi.Registros);
            }

            Consultorios = new List<DisponibilidadConsultorio>(listaConsultorios);

            IsBusy = false;
        }

        private async void CargarPacientes()
        {
            var resultadoApi = await DocNocApi.Citas.TraeUsuarioAdicional(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            var lista3 = new List<ResultadoUsuarioAdicional>();

            lista3.Add(new ResultadoUsuarioAdicional() { IdUsuarioAdicional = string.Empty, NombreAdicional = "Yo" });

            if (!resultadoApi.Error)
            {
                foreach (var paciente in resultadoApi.Registros)
                {
                    lista3.Add(paciente);
                }

                Pacientes = new List<ResultadoUsuarioAdicional>(lista3);
            }
        }

        private void Retroceder()
        {
            if (Seccion3Activa)
            {
                Seccion1Activa = false;
                Seccion2Activa = true;
                Seccion3Activa = false;
                AvanzarVisible = true;
                RetrocederVisible = true;
                AgendarVisible = false;
                Step1 = StepStatus.Completed;
                Step2 = StepStatus.InProgress;
                Step3 = StepStatus.NotStarted;
            }
            else
            {
                if (Seccion2Activa)
                {
                    Step1 = StepStatus.InProgress;
                    Step2 = StepStatus.NotStarted;
                    Step3 = StepStatus.NotStarted;
                    Seccion1Activa = true;
                    Seccion2Activa = false;
                    Seccion3Activa = false;
                    AvanzarVisible = true;
                    RetrocederVisible = false;
                    AgendarVisible = false;
                }
            }
        }

        private void Avanzar()
        {
            if (Seccion1Activa)
            {
                if(HorarioSeleccionado == null)
                {
                    Dialog.Show("Error", "Seleccione un horario para la consulta.", "Aceptar");
                    return;
                }

                ConsultorioSeleccionado = ObtenerConsultorioSeleccionado();
                Seccion1Activa = false;
                Seccion2Activa = true;
                Seccion3Activa = false;
                Step1 = StepStatus.Completed;
                Step2 = StepStatus.InProgress;
                Step3 = StepStatus.NotStarted;
                AvanzarVisible = true;
                RetrocederVisible = true;
                AgendarVisible = false;
            }
            else
            {
                if (Seccion2Activa)
                {
                    if (PacienteSeleccionado == null && string.IsNullOrWhiteSpace(PacienteExterno))
                    {
                        Dialog.Show("Error", "Seleccione un paciente para la consulta.", "Aceptar");
                        return;
                    }

                    //if (string.IsNullOrWhiteSpace(MotivoConsulta))
                    //{
                    //    Dialog.Show("Error", "Por favor indique un motivo para la cita.", "Aceptar");
                    //    return;
                    //}

                    //if (MotivoConsulta.Length < 6)
                    //{
                    //    Dialog.Show("Error", "El motivo debe tener una longitud de 6 caracteres o más.", "Aceptar");
                    //    return;
                    //}

                    if (!string.IsNullOrWhiteSpace(PacienteExterno))
                    {
                        PacienteCita = $"Cita para {PacienteExterno}";
                    }
                    else
                    {
                        if(PacienteSeleccionado.IdUsuarioAdicional == string.Empty)
                            PacienteCita = $"Cita para mí";
                        else
                            PacienteCita = $"Cita para {PacienteSeleccionado.NombreAdicional}";
                    }

                    CitaLetra = HorarioSeleccionado.FechaCitaI.ToString("MMMM dd, hh:mm tt", 
                        CultureInfo.CreateSpecificCulture("es-ES")).ToUpper();
                    
                    Step1 = StepStatus.Completed;
                    Step2 = StepStatus.Completed;
                    Step3 = StepStatus.InProgress;
                    Seccion1Activa = false;
                    Seccion2Activa = false;
                    Seccion3Activa = true;
                    AvanzarVisible = false;
                    RetrocederVisible = true;
                    AgendarVisible = true;
                }
            }
        }

        private DisponibilidadConsultorio ObtenerConsultorioSeleccionado()
        {
            return Consultorios.Find(x => x.IdConsultorio == HorarioSeleccionado.IdConsultorio);
        }

        private async void AgendarCita()
        {
            GenerandoCita = true;

            var citaNueva = new ParaCrearCita()
            {
                IdConsultorio = HorarioSeleccionado.IdConsultorio,
                FechaCitaI = HorarioSeleccionado.FechaCitaI,
                IdUsuarioPaciente = idUsuario,
                IdUsuarioDoctor = IdMedico,
                TipoCita = "Cita",
                Motivo = MotivoConsulta,
                IdEspecialidad = 1                
            };

            if (!string.IsNullOrWhiteSpace(PacienteExterno))
            {
                citaNueva.NombreInvitado = PacienteExterno;
            }
            else
            {
                if(PacienteSeleccionado != null && PacienteSeleccionado.IdUsuarioAdicional != string.Empty)
                {
                    citaNueva.IdUsuarioAdicional = PacienteSeleccionado.IdUsuarioAdicional;
                }
            }

            var respuestaApi = await DocNocApi.Citas.CreaCita(citaNueva);

            GenerandoCita = false;

            if (respuestaApi.Error)
            {
                await Dialog.Show("Error", $"No se pudo agendar la cita: {respuestaApi.CadenaErrores()} {respuestaApi.MensajeCodigo}", "Aceptar");
            }
            else
            {
                await Dialog.Show("Éxito", $"La cita ha sido agendada.", "Aceptar");

                Regresar();
            }
        }

        //private void SeleccionarHorario(Cita)
        //{
            
        //}

        #endregion

        #region Command

        public Command BackwardsCommand { get; set; }

        public Command FinishCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the edit button is clicked.
        /// </summary>
        public Command NextCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the delete button is clicked.
        /// </summary>
        public Command DeleteCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the add card button is clicked.
        /// </summary>
        public Command AddCardCommand { get; set; }

        public Command HorarioSeleccionadoCommand { get; set; }

        #endregion
    }
}
