using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models;
using DocNoc.Xam.Models.Consultorios;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using PPS.Estandar;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Principal
{
    /// <summary>
    /// Definición de ViewModel: Buscar Médico (dn-11-3)
    /// </summary>
    public class BuscarMedicoViewModel : DocNocViewModel
    {
        #region Constructor

        public BuscarMedicoViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Home (dn-07-3).
            //PageText = text.Get<PaginaTxt>("dn-07-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            this.PopupCommand = new Command<SfPopupLayout>(AbrirPopup);
            this.RefreshCommand = new Command(CargarDatos);
            this.BusquedaCommand = new Command<Especialidad>(Busqueda);
            this.BusquedaAvanzadaCommand = new Command(BusquedaAvanzada);
            this.CancelarBusquedaAvanzadaCommand = new Command(CargarDatos);
            this.NavegarMapaCommand = new Command(NavegarMapa);

            IsBusy = false;
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

        public List<EstadosCiudades> Ciudades
        {
            get { return this._ciudades; }
            set { SetProperty(ref _ciudades, value); }
        }
        private List<EstadosCiudades> _ciudades;

        public List<Especialidad> Especialidades
        {
            get { return this.especialidades; }
            set { SetProperty(ref especialidades, value); }
        }
        private List<Especialidad> especialidades;

        public List<string> Disponibilidad
        {
            get { return this.disponibilidad; }
            set { SetProperty(ref disponibilidad, value); }
        }
        private List<string> disponibilidad;

        public List<Aseguradora> Aseguradoras
        {
            get { return this.aseguradoras; }
            set { SetProperty(ref aseguradoras, value); }
        }
        private List<Aseguradora> aseguradoras;

        public string Ubicacion
        {
            get { return this.ubicacion; }
            set { SetProperty(ref ubicacion, value); }
        }
        private string ubicacion;

        public EstadosCiudades CiudadSeleccionada
        {
            get { return this._ciudadSeleccionada; }
            set { SetProperty(ref _ciudadSeleccionada, value); }
        }
        private EstadosCiudades _ciudadSeleccionada;

        public Especialidad EspecialidadSeleccionada
        {
            get { return this.especialidadSeleccionada; }
            set { SetProperty(ref especialidadSeleccionada, value); }
        }
        private Especialidad especialidadSeleccionada;

        public string DisponibilidadSeleccionada
        {
            get { return this.disponibilidadSeleccionada; }
            set { SetProperty(ref disponibilidadSeleccionada, value); }
        }
        private string disponibilidadSeleccionada;

        public Aseguradora AseguradoraSeleccionada
        {
            get { return this.aseguradoraSeleccionada; }
            set { SetProperty(ref aseguradoraSeleccionada, value); }
        }
        private Aseguradora aseguradoraSeleccionada;

        public bool Rating0Seleccionado
        {
            get { return this.rating0Seleccionado; }
            set { SetProperty(ref rating0Seleccionado, value); }
        }
        private bool rating0Seleccionado;

        public bool Rating3Seleccionado
        {
            get { return this.rating3Seleccionado; }
            set { SetProperty(ref rating3Seleccionado, value); }
        }
        private bool rating3Seleccionado;

        public bool Rating4Seleccionado
        {
            get { return this.rating4Seleccionado; }
            set { SetProperty(ref rating4Seleccionado, value); }
        }
        private bool rating4Seleccionado;

        public bool Rating5Seleccionado
        {
            get { return this.rating5Seleccionado; }
            set { SetProperty(ref rating5Seleccionado, value); }
        }
        private bool rating5Seleccionado;

        public bool ConfirmacionInmediata
        {
            get { return this.confirmacionInmediata; }
            set { SetProperty(ref confirmacionInmediata, value); }
        }
        private bool confirmacionInmediata;

        #endregion

        #region Commands

        public Command RefreshCommand { get; set; }
        public Command BusquedaCommand { get; set; }
        public Command BusquedaAvanzadaCommand { get; set; }
        public Command CancelarBusquedaAvanzadaCommand { get; set; }
        public Command NavegarMapaCommand { get; set; }

        #endregion

        #region Methods

        private void Busqueda(Especialidad especialidadSeleccionada)
        {
            if (especialidadSeleccionada is null)
            {
                return;
            }

            if (CiudadSeleccionada is null || string.IsNullOrWhiteSpace(CiudadSeleccionada.EstadoCiudad))
            {
                MensajeError("Especifique una ciudad.");
                return;
            }

            var datosBusqueda = new BusquedaMedicoAPP()
            {
                CiudadOCP = CiudadSeleccionada.EstadoCiudad,
                IdEspecialidad = especialidadSeleccionada.IdEspecialidad,
                Cita = string.Empty,
                IdAseguradora = null,
                Calificacion = null,
                AceptaCitaAutomaticamente = null
            };

            Preferences.Set("BuscarMedico_Filtro", SerializarJson(datosBusqueda));

            EjecutarBusqueda();
        }

        private void BusquedaAvanzada()
        {
            if (CiudadSeleccionada is null || string.IsNullOrWhiteSpace(CiudadSeleccionada.EstadoCiudad))
            {
                MensajeError("Especifique una ciudad.");
                return;
            }

            if (EspecialidadSeleccionada == null)
            {
                MensajeError("Especifique una especialidad médica.");
                return;
            }

            string disponibilidadCita = "Hoy";

            if(DisponibilidadSeleccionada == "Esta Semana")
                disponibilidadCita = "Semana";

            if (DisponibilidadSeleccionada == "Este Mes")
                disponibilidadCita = "Mes";

            var datosBusqueda = new BusquedaMedicoAPP()
            {
                CiudadOCP = CiudadSeleccionada.EstadoCiudad,
                IdEspecialidad = EspecialidadSeleccionada.IdEspecialidad,
                Cita = disponibilidadCita
            };

            int filtroRating = 0;

            if (rating3Seleccionado)
                filtroRating = 3;

            if (rating4Seleccionado)
                filtroRating = 4;

            if (rating5Seleccionado)
                filtroRating = 5;

            if (filtroRating != 0)
            {
                datosBusqueda.Calificacion = filtroRating;
            }

            if(AseguradoraSeleccionada.IdAseguradora != 0)
            {
                datosBusqueda.IdAseguradora = AseguradoraSeleccionada.IdAseguradora;
            }

            if(ConfirmacionInmediata)
            {
                datosBusqueda.AceptaCitaAutomaticamente = true;
            }

            Preferences.Set("BuscarMedico_Filtro", SerializarJson(datosBusqueda));

            EjecutarBusqueda();
        }

        private void EjecutarBusqueda()
        {
            Navigation.NavigateTo(typeof(ViewModels.Medicos.ListaMedicosViewModel), string.Empty, string.Empty);
        }

        private async void CargarDatos()
        {
            IsBusy = true;

            Disponibilidad = new List<string>() { "Hoy", "Esta Semana", "Este Mes" };

            DisponibilidadSeleccionada = Disponibilidad.First();

            //Carga de Ciudades
            var respuestaCiudades = await DocNocApi.Consultorios.TraeCiudadesConsultorio();

            if (respuestaCiudades.Error)
            {
                ErrorEntidad(respuestaCiudades);
                return;
            }

            Ciudades = new List<EstadosCiudades>(respuestaCiudades.Registros);

            //Carga de Aseguradoras
            var respuestaAseguradoras = await DocNocApi.Aseguradoras.ListadoAseguradoras();

            if (respuestaAseguradoras.Error)
            {
                ErrorEntidad(respuestaAseguradoras);
                return;
            }

            var listaAseguradoras = new List<Aseguradora>()
            {
                new Aseguradora()
                {
                    IdAseguradora = 0,
                    NombreAseguradora = "(Todas)"
                }
            };

            listaAseguradoras.AddRange(respuestaAseguradoras.Registros);

            Aseguradoras = new List<Aseguradora>(listaAseguradoras);

            AseguradoraSeleccionada = Aseguradoras.First();

            //Carga de Especialidades
            var respuestaEspecialidades = await DocNocApi.Especialidades.ListadoEspecialidades();

            if (respuestaEspecialidades.Error)
            {
                ErrorEntidad(respuestaEspecialidades);
                return;
            }

            Especialidades = new List<Especialidad>(respuestaEspecialidades.Registros);

            //Se carga la lista de ciudades.

            Rating0Seleccionado = true;
            Rating3Seleccionado = false;
            Rating4Seleccionado = false;
            Rating5Seleccionado = false;

            ConfirmacionInmediata = false;

            IsBusy = false;
        }

        private void NavegarMapa()
        {
            Navigation.NavigateTo(typeof(ViewModels.Medicos.MapaMedicosViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            CargarDatos();
        }

        #endregion
    }
}

