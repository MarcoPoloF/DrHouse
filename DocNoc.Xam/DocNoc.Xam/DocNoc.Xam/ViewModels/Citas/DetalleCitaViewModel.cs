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
    /// Definición de ViewModel: Detalle de Cita (dn-25-3).
    /// </summary>
    public class DetalleCitaViewModel : DocNocViewModel
    {

        #region Constructor

        public DetalleCitaViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            IsRefreshing = false;

            this.BackCommand = new Command(Regresar);
            this.CancelCommand = new Command(CancelarCita);
            this.EditCommand = new Command(EditarCita);
            this.NotifyDoctorCommand = new Command(NotificarLlegada);
            this.PopupCommand = new Command<SfPopup>(AbrirPopup);
            this.RefreshCommand = new Command(CargarCita);
            this.ShareFilesCommand = new Command(CompartirArchivos);
            this.NotificarLlegadaCommand = new Command(NotificarLlegada);

        }

        #endregion

        #region Fields

        //private PaginaTxt pageText;

        private ResultadoMiCita cita;

        private bool isRefreshing;

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

        public UsuarioAPP Medico
        {
            get { return this.medico; }
            set { SetProperty(ref medico, value); }
        }
        private UsuarioAPP medico;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command to be executed when the user wants to cancel the appointment.
        /// </summary>
        public Command CancelCommand { get; set; }       

        /// <summary>
        /// Gets or sets the command to be executed when the user wants to edit the appointment.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the user notifies the doctor of their arrival.
        /// </summary>
        public Command NotifyDoctorCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the page is refreshed.
        /// </summary>
        public Command RefreshCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the user wants to share files.
        /// </summary>
        public Command ShareFilesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the user wants to share files.
        /// </summary>
        public Command NotificarLlegadaCommand { get; set; }        

        #endregion

        #region Methods

        private async void CancelarCita()
        {
            var respuestaApi = await DocNocApi.Citas.CancelaCitaPaciente(cita.IdCita);

            if (respuestaApi.Error)
            {
                await Dialog.Show("Error", respuestaApi.Mensajes[0].Contenido, "Aceptar");
                return;
            }

            await Dialog.Show("Éxito", "La cita ha sido cancelada.", "Aceptar");

            //await Task.Delay(5000);

            Regresar();
        }

        private async void CargarCita()
        {
            Int32.TryParse(Preferences.Get("IdCita"), out int idCita);

            var respuestaApi = await DocNocApi.Citas.MiCita(idCita);

            if (respuestaApi.Error)
            {
                return;
            }

            if(respuestaApi.Registros.Count == 0)
            {
                ErrorEntidad(respuestaApi);
                return;
            }

            Cita = respuestaApi.Registros.First();

            if(cita.Estatus == "CanceladaPaciente")
            {
                Regresar();
            }

            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioProAPP(new ParaFiltroUsuario() { IdUsuario = Cita.IdUsuarioDoctor });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Medico = resultadoApi;
        }

        private async void CompartirArchivos()
        {
            if (Cita.EsPacienteExterno)
            {
                await Dialog.Show("No Disponible", "Esta función no está disponible para citas de pacientes externos", "Aceptar");
                return;
            }

            Preferences.Set("CompartirArchivos_Cita", "true");
            Preferences.Set("IdExpediente_CompartirArchivos", cita.IdUsuarioPaciente);
            Preferences.Set("IdMedico_CompartirArchivos", cita.IdUsuarioDoctor);

            Navigation.NavigateTo(typeof(ViewModels.Expedientes.EstudiosAnalisisViewModel), string.Empty, string.Empty);
        }

        private void EditarCita()
        {
            Preferences.Set("IdCita", cita.IdCita.ToString());

            Navigation.NavigateTo(typeof(ViewModels.Citas.EditarCitaViewModel), string.Empty, string.Empty);
        }

        private async void NotificarLlegada()
        {
            var datosLlegda = new ParaAvisaLlegada()
            {
                IdCita = Cita.IdCita,
                IdUsuarioEnvia = idUsuario,
                IdUsuarioRecibe = Cita.IdUsuarioDoctor
            };

            var resultadoApi = await DocNocApi.Mensajes.AvisaLlegada(datosLlegda);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            await Dialog.Show("Éxito", "Su médico ha sido notificado de su llegada.", "Aceptar");
        }

        public void OnAppearing()
        {
            CargarCita();
        }

        #endregion

    }
}
