using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Citas
{
    /// <summary>
    /// Definición de ViewModel: NOMBRE_PAGINA (dn-##-3).
    /// </summary>
    public class EditarCitaViewModel : DocNocViewModel
    {
        #region Constructor

        public EditarCitaViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.ConfirmCommand = new Command(ModificarCita);
            this.PopupCommand = new Command<SfPopup>(AbrirPopup);

            CargarCita();

            //CargarHorarios();
        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;
        
        private ResultadoMiCita cita;

        private List<CitaConsultorio> citasDisponibles;

        private CitaConsultorio nuevaCita;

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

        public ResultadoMiCita Cita
        {
            get { return this.cita; }
            set { SetProperty(ref cita, value); }
        }

        public List<CitaConsultorio> CitasDisponibles
        {
            get { return this.citasDisponibles; }
            set { SetProperty(ref citasDisponibles, value); }
        }

        public CitaConsultorio NuevaCita
        {
            get { return this.nuevaCita; }
            set { SetProperty(ref nuevaCita, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command ConfirmCommand { get; set; }

        #endregion

        #region Methods

        private async void CargarCita()
        {
            Int32.TryParse(Preferences.Get("IdCita"), out int idCita);

            var respuestaApi = await DocNocApi.Citas.MiCita(idCita);

            if (respuestaApi.Error)
            {
                return;
            }

            if (respuestaApi.Registros.Count == 0)
            {
                await Dialog.Show("Error", "No se recuperó información de la cita.", "Aceptar");
                return;
            }

            Cita = respuestaApi.Registros.First();

            if (cita.Estatus == "CanceladaPaciente")
            {
                Regresar();
                return;
            }

            CargarHorarios();
        }

        private async void CargarHorarios()
        {
            var resultadoApi = await DocNocApi.Citas.TraeAgendaMedicoAPP
                (new ParaFiltroUsuarioConsultorio() { IdUsuario = Cita.IdUsuarioDoctor, IdConsultorio = Cita.IdConsultorio });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            CitasDisponibles = new List<CitaConsultorio>(resultadoApi.Registros);

            NuevaCita = CitasDisponibles.First();
        }

        private async void ModificarCita()
        {
            var resultadoApi = await DocNocApi.Citas.PosponeCitaPaciente
                (new ParaPosponerCita() { IdCita = cita.IdCita, FechaCitaI = nuevaCita.FechaCitaI });

            if (resultadoApi.Error)
            {
                await Dialog.Show("Error", $"No se pudo modificar la cita: {resultadoApi.CadenaErrores()}", "Aceptar");
                return;
            }

            await Dialog.Show("Modificación de Cita Enviada", "El médico ha recibido tu solicitud de modificación de la cita. Cuando la acepte, podrás verla en MIS CITAS como una Cita Confirmada.", "Cerrar");

            //await Task.Delay(5000);

            Regresar();
        }

        #endregion
    }
}
