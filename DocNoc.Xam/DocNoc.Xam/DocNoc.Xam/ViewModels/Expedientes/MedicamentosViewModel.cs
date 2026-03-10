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
    /// Definición de ViewModel: Medicamentos (dn-49-3).
    /// </summary>
    public class MedicamentosViewModel : DocNocViewModel
    {
        #region Constructor

        public MedicamentosViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.AddCommand = new Command(InsertarEvento);
            this.IrSuscripcionCommand = new Command(AbrirSuscripcion);
            this.AgregarPopupCommand = new Command(AgregarRegistro);
            this.AgregarPopupCommand2 = new Command<object>(AgregarRegistro2);
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

        public int IdMedicamento
        {
            get { return this.idMedicamento; }
            set { SetProperty(ref idMedicamento, value); }
        }

        private int idMedicamento;

        public List<MiMedicamentoAPP> Registros
        {
            get { return this.registros; }
            set { SetProperty(ref registros, value); }
        }
        private List<MiMedicamentoAPP> registros;

        public DateTime FechaEvento
        {
            get { return this.fechaEvento; }
            set { SetProperty(ref fechaEvento, value); }
        }
        private DateTime fechaEvento;

        public string Medicamento
        {
            get { return this.medicamento; }
            set { SetProperty(ref medicamento, value); }
        }
        private string medicamento;

        public string Dosis
        {
            get { return this.dosis; }
            set { SetProperty(ref dosis, value); }
        }
        private string dosis;

        public string CuidadoRecomendacion
        {
            get { return this.cuidadoRecomendacion; }
            set { SetProperty(ref cuidadoRecomendacion, value); }
        }
        private string cuidadoRecomendacion;

        public string TomarCada
        {
            get { return this.tomarCada; }
            set { SetProperty(ref tomarCada, value); }
        }
        private string tomarCada;

        public string Duracion
        {
            get { return this.duracion; }
            set { SetProperty(ref duracion, value); }
        }
        private string duracion;

        public TimeSpan HoraToma
        {
            get { return this.horaToma; }
            set { SetProperty(ref horaToma, value); }
        }
        private TimeSpan horaToma;

        public bool AgregarVisible
        {
            get { return this.agregarVisible; }
            set { SetProperty(ref agregarVisible, value); }
        }
        private bool agregarVisible;

        #endregion

        #region Commands

        public Command AddCommand { get; set; }

        public Command AgregarPopupCommand { get; set; }

        public Command<object> AgregarPopupCommand2 { get; set; }

        #endregion

        #region Methods

        private async void AgregarRegistro()
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            IdMedicamento = -1;
            var filtro = new ParaFiltroUsuarioyDato()
            {
                IdUsuario = idUsuario,
                Dato = "MedicamentosProgramarMedicamento"
            };

            var resultadoApi = await DocNocApi.OpenPay.PreValidacionPaciente(filtro);

            if (resultadoApi.Error)
            {
                string[] errorArray = resultadoApi.Mensajes[0].Contenido.Split('|');

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

        private async void AgregarRegistro2(object obj)
        {
            if (mockSuscriptionError)
            {
                ErrorSuscripcion = "Mocked: Suscription error";
                EnlaceSuscripcion = "https://docnoc.mx";
                ErrorSuscripcionVisible = true;
                return;
            }

            var filtro = new ParaFiltroUsuarioyDato()
            {
                IdUsuario = idUsuario,
                Dato = "MedicamentosProgramarMedicamento"
            };

            var resultadoApi = await DocNocApi.OpenPay.PreValidacionPaciente(filtro);

            if (resultadoApi.Error)
            {
                string[] errorArray = resultadoApi.Mensajes[0].Contenido.Split('|');

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

            var x = obj as MiMedicamentoAPP;

            IdMedicamento = x.IdMedicamento;
            Medicamento = x.Medicamento;
            Dosis = x.Dosis;
            CuidadoRecomendacion = x.CuidadoRecomendacion;

            AgregarVisible = true;
        }

        private async void InsertarEvento()
        {
            if (string.IsNullOrWhiteSpace(Medicamento))
            {
                MensajeError("Introduzca el nombre del medicamento.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Dosis))
            {
                MensajeError("Introduzca la dosis del medicamento.");
                return;
            }

            if (!byte.TryParse(TomarCada, out byte tomarCadaByte))
            {
                MensajeError("Intervalo de toma inválido.");
                return;
            }

            if (!byte.TryParse(Duracion, out byte duracionByte))
            {
                MensajeError("Duración inválida.");
                return;
            }

            //var fechaActual = DateTime.Now.Date;
            //fechaActual = fechaActual.AddSeconds(HoraToma.TotalSeconds);
            var fechaDispositivo = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var fechaProgramacion = DateTime.SpecifyKind(DateTime.Now.Date.AddSeconds(HoraToma.TotalSeconds), DateTimeKind.Unspecified);

            if (IdMedicamento != -1)
            {
                var medicamentoProgramacion = new InsertarMedicamentoProgramacion2()
                {
                    Medicamento = Medicamento,
                    Dosis = Dosis,
                    CuidadoRecomendacion = string.Empty,
                    FechaPrimerToma = fechaProgramacion,
                    TomarCada = tomarCadaByte,
                    Duracion = duracionByte,
                    IdMedicamento = IdMedicamento,
                    FechaDispositivo = fechaDispositivo
                };

                if (EsUsuarioAdicional)
                {
                    medicamentoProgramacion.IdUsuario = idUsuario;
                    medicamentoProgramacion.IdUsuarioAdicional = IdExpediente;
                }
                else
                {
                    medicamentoProgramacion.IdUsuarioPrincipal = idUsuario;
                }

                var resultadoApiID = await DocNocApi.Pacientes.InsertaMedicamentoProgramacion(medicamentoProgramacion);

                if (resultadoApiID.Error)
                {
                    ErrorEntidad(resultadoApiID);
                    return;
                }
            }
            else
            {
                var medicamento = new InsertarMiMedicamento2()
                {
                    //IdUsuario = IdExpediente,
                    Medicamento = Medicamento,
                    Dosis = Dosis,
                    CuidadoRecomendacion = string.Empty,
                    FechaPrimerToma = fechaProgramacion,
                    TomarCada = tomarCadaByte,
                    Duracion = duracionByte,
                    FechaDispositivo = fechaDispositivo
                };

                if (EsUsuarioAdicional)
                {
                    medicamento.IdUsuario = idUsuario;
                    medicamento.IdUsuarioAdicional = IdExpediente;
                }
                else
                {
                    medicamento.IdUsuarioPrincipal = idUsuario;
                }

                var resultadoApi = await DocNocApi.Pacientes.InsertaMiMedicamento(medicamento);

                if (resultadoApi.Error)
                {
                    ErrorEntidad(resultadoApi);
                    return;
                }
            }
            CargarDatos();
        }

        private async void CargarDatos()
        {
            FechaEvento = DateTime.Now.Date;
            HoraToma = new TimeSpan(0,0,0);
            Medicamento = null;
            Dosis = null;
            CuidadoRecomendacion = null;

            var datosPaciente = new ParaFiltroUsuarioyAdicional() { IdUsuario = idUsuario };

            if (EsUsuarioAdicional)
                datosPaciente.IdUsuarioAdicional = IdExpediente;

            var resultadoApi = await DocNocApi.Pacientes.MisMedicamentosAPP(datosPaciente);

            if (resultadoApi.Error)
            {
                MensajeError(resultadoApi.CadenaErrores());
                return;
            }

            foreach(var medicamento in resultadoApi.Registros)
            {
                if (string.IsNullOrWhiteSpace(medicamento.Medico))
                {
                    medicamento.Medico = "(ninguno)";
                    medicamento.TraeMedico = false;
                }
                else
                {
                    medicamento.TraeMedico = true;
                }

                if (string.IsNullOrWhiteSpace(medicamento.CuidadoRecomendacion))
                    medicamento.CuidadoRecomendacion = "(ninguno)";
            }

            Registros = new List<MiMedicamentoAPP>(resultadoApi.Registros);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}
