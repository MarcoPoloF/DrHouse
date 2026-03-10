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

namespace DocNoc.Xam.ViewModels.Consultas
{
    /// <summary>
    /// Definición de ViewModel: Alergias (dn-38-3).
    /// </summary>
    public class DetalleConsultaViewModel : DocNocViewModel
    {
        #region Constructor

        public DetalleConsultaViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
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
            this.CalificarCommand = new Command(CalificarConsulta);
            this.CancelarCalificarCommand = new Command(CargarConsulta);
            this.NavegarAgendaMedicoCommand = new Command(AgendarCita);

        }

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
        //private PaginaTxt pageText;

        public DetalleConsulta Consulta
        {
            get { return this.consulta; }
            set { SetProperty(ref consulta, value); }
        }
        private DetalleConsulta consulta;

        public double Calificacion1
        {
            get { return this.calificacion1; }
            set { SetProperty(ref calificacion1, value); }
        }
        private double calificacion1;

        public double Calificacion2
        {
            get { return this.calificacion2; }
            set { SetProperty(ref calificacion2, value); }
        }
        private double calificacion2;

        public double Calificacion3
        {
            get { return this.calificacion3; }
            set { SetProperty(ref calificacion3, value); }
        }
        private double calificacion3;

        public string ComentarioCalificacion
        {
            get { return this.comentarioCalificacion; }
            set { SetProperty(ref comentarioCalificacion, value); }
        }
        private string comentarioCalificacion;

        public bool ConsultaNoCalificada
        {
            get { return this.consultaNoCalificada; }
            set { SetProperty(ref consultaNoCalificada, value); }
        }
        private bool consultaNoCalificada;

        #endregion

        #region Commands

        public Command CalificarCommand { get; set; }
        public Command CancelarCalificarCommand { get; set; }
        public Command NavegarAgendaMedicoCommand { get; set; }

        #endregion

        #region Methods

        private void AgendarCita()
        {
            Preferences.Set("PerfilMedico_Id", Consulta.IdUsuarioDoctor);
            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        private async void CalificarConsulta()
        {
            if(Calificacion1 == 0 || Calificacion2 == 0 || Calificacion3 == 0)
            {
                MensajeError("Debe llenar todos los campos de calificación.");
                return;
            }

            var calificacionConsulta = new CalificaCitaConComentario()
            {
                IdUsuarioPaciente = idUsuario,
                IdUsuarioDoctor = Consulta.IdUsuarioDoctor,
                IdCita = Consulta.IdCita,
                IdPregunta1 = 4,
                IdPregunta2 = 5,
                IdPregunta3 = 6,
                Calificacion1 = (int)Calificacion1,
                Calificacion2 = (int)Calificacion2,
                Calificacion3 = (int)Calificacion3,
                Comentario = ComentarioCalificacion
            };

            var respuestaApi = await DocNocApi.Citas.CalificaCitaalDoctor(calificacionConsulta);

            if (respuestaApi.Error)
            {
                await Dialog.Show("Error", $"No se pudo calificar la consulta: {respuestaApi.MensajeCodigo}", "Aceptar");
                return;
            }

            CargarConsulta();
        }

        private async void CargarConsulta()
        {
            Calificacion1 = 0;
            Calificacion2 = 0;
            Calificacion3 = 0;
            ComentarioCalificacion = string.Empty;

            if(!int.TryParse(Preferences.Get("IdConsulta"), out int idConsulta))
            {
                MensajeError($"Error al cargar la consulta. Valor '{Preferences.Get("idexpediente")}'");
                return;
            }

            var resultadoApi = await DocNocApi.Usuarios.TraeHistoriaConsultaDetalleAPP(idConsulta);

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Consulta = resultadoApi.Contenido;

            ConsultaNoCalificada = !Consulta.Calificada;
        }

        public void OnAppearing()
        {
            CargarConsulta();
        }

        #endregion
    }
}
