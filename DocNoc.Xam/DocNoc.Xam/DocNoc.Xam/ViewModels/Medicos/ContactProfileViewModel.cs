using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Model = DocNoc.Xam.Models.Profile;
using Syncfusion.XForms.Border;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using System;
using System.Globalization;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ContactProfileViewModel : DocNocViewModel
    {
        #region Field

        private ObservableCollection<Model> profileInfo;

        private UsuarioAPP medico;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ContactProfileViewModel" /> class.
        /// </summary>
        public ContactProfileViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            this.ProfileInfo = new ObservableCollection<Model>();

            for (var i = 0; i < 6; i++)
            {
                this.ProfileInfo.Add(new Model { ImagePath = "ProfileImage1" + i + ".png" });
            }

            this.BackCommand = new Command(Regresar);
            this.ProfileNameCommand = new Command(this.ProfileNameClicked);
            this.EditCommand = new Command(this.EditButtonClicked);
            this.ViewAllCommand = new Command(this.ViewAllButtonClicked);
            this.MediaImageCommand = new Command(this.MediaImageClicked);
            this.AgendarCitaCommand = new Command(this.AgendarCita);
            this.AbrirChatCommand = new Command(this.AbrirChat);
            this.AgregarFavoritoCommand = new Command(this.AgregarFavorito);
            this.RemoverFavoritoCommand = new Command(this.RemoverFavorito);
            this.ShareCommand = new Command(this.CompartirPerfil);
            this.IrServiciosCommand = new Command(this.IrServicios);
            this.IrOpinionesCommand = new Command(this.IrOpiniones);

            MostrarEstatusFavorito = false;
            EsFavorito = false;

            MostrarProximaCita = false;

            ChatDisponible = false;
            ColorEstatus = (Color)Application.Current.Resources["Red"];
            EstatusLetra = "Desconectado";
        }

        #endregion

        #region Public Property

        private string idMedico;

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        public ObservableCollection<Model> ProfileInfo
        {
            get
            {
                return this.profileInfo;
            }

            set
            {
                SetProperty(ref profileInfo, value);
            }
        }

        public UsuarioAPP Medico
        {
            get { return this.medico; }
            set { SetProperty(ref medico, value); }
        }

        public List<UsuarioAseguradora> Aseguradoras
        {
            get { return this.aseguradoras; }
            set { SetProperty(ref aseguradoras, value); }
        }
        private List<UsuarioAseguradora> aseguradoras;

        public List<DisponibilidadConsultorio> Consultorios
        {
            get { return this.consultorios; }
            set { SetProperty(ref consultorios, value); }
        }
        private List<DisponibilidadConsultorio> consultorios;

        public bool ChatDisponible
        {
            get { return this.chatDisponible; }
            set { SetProperty(ref chatDisponible, value); }
        }
        private bool chatDisponible;

        public string ProximaCita
        {
            get { return this.proximaCita; }
            set { SetProperty(ref proximaCita, value); }
        }
        private string proximaCita;

        public string EstatusLetra
        {
            get { return this.estatusLetra; }
            set { SetProperty(ref estatusLetra, value); }
        }
        private string estatusLetra;

        public Color ColorEstatus
        {
            get { return this.colorEstatus; }
            set { SetProperty(ref colorEstatus, value); }
        }
        private Color colorEstatus;

        public bool MostrarEstatusFavorito
        {
            get { return this.mostrarEstatusFavorito; }
            set { SetProperty(ref mostrarEstatusFavorito, value); }
        }
        private bool mostrarEstatusFavorito;

        public bool MostrarProximaCita
        {
            get { return this.mostrarProximaCita; }
            set { SetProperty(ref mostrarProximaCita, value); }
        }
        private bool mostrarProximaCita;

        public bool EsFavorito
        {
            get { return this.esFavorito; }
            set { SetProperty(ref esFavorito, value); }
        }
        private bool esFavorito;

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command AbrirChatCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command AgendarCitaCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the view all button is clicked.
        /// </summary>
        public Command ViewAllCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the media image is clicked.
        /// </summary>
        public Command MediaImageCommand { get; set; }

        public Command AgregarFavoritoCommand { get; set; }

        public Command RemoverFavoritoCommand { get; set; }

        public Command ShareCommand { get; set; }

        public Command IrServiciosCommand { get; set; }

        public Command IrOpinionesCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the profile name is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void ProfileNameClicked(object obj)
        {
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as SfBorder).BackgroundColor = (Color)retVal;
            await Task.Delay(100);

            Application.Current.Resources.TryGetValue("Gray-White", out var oldVal);
            (obj as SfBorder).BackgroundColor = (Color)oldVal;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the view all button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ViewAllButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the media image is clicked.
        /// </summary>
        private void MediaImageClicked(object obj)
        {
            // Do something
        }

        private async void CompartirPerfil()
        {
            await Share.RequestAsync(new ShareTextRequest()
            {
                //Uri = Medico.RutaPerfil,
                Text = Medico.RutaPerfil,
                Title = $"Doc+Noc: {Medico.TituloNombreCompleto}"
            });
        }

        private async void AgregarFavorito()
        {
            var resultadoApi = await DocNocApi.Pacientes.AgregaFavorito(new Favorito() { IdUsuario = idUsuario, IdUsuarioDoctor = idMedico });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            CargarEstatusFavorito();
        }

        private async void RemoverFavorito()
        {
            var resultadoApi = await DocNocApi.Pacientes.EliminaFavorito(new Favorito() { IdUsuario = idUsuario, IdUsuarioDoctor = idMedico });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            CargarEstatusFavorito();
        }

        private async void CargarAseguradoras()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioAseguradora(new ParaFiltroUsuario() { IdUsuario = idMedico });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Aseguradoras = new List<UsuarioAseguradora>(resultadoApi.Registros);
        }

        private async void CargarConsultorios()
        {
            var consultoriosApi = await DocNocApi.Consultorios.TraeMisConsultoriosParaAPP(new ParaFiltroUsuario() { IdUsuario = idMedico });

            if (consultoriosApi.Error)
            {
                ErrorEntidad(consultoriosApi);
                return;
            }

            Consultorios = new List<DisponibilidadConsultorio>(consultoriosApi.Registros);
        }

        private async void CargarProximaCita()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioProAPPProximaCita(new ParaFiltroUsuario() { IdUsuario = idMedico });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            var fechaProximaCita = resultadoApi.Contenido.ProximaCita;

            string fechaLetra = String.Empty;

            if (fechaProximaCita.Date > DateTime.Now.Date)
            {
                if ((fechaProximaCita.Date - DateTime.Now.Date).TotalDays == 1)
                    fechaLetra += "Mañana, ";
                else
                    fechaLetra += fechaProximaCita.ToString("MMMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }
            else
            {
                if (fechaProximaCita.Date == DateTime.Now.Date)
                    fechaLetra += "Hoy, ";
                else
                    fechaLetra += fechaProximaCita.ToString("MMMM d, ", CultureInfo.CreateSpecificCulture("es-ES"));
            }

            fechaLetra += fechaProximaCita.ToString("h:mm tt");

            ProximaCita = fechaLetra;
            MostrarProximaCita = true;
        }

        private async void CargarEstatusFavorito()
        {
            var resultadoApi = await DocNocApi.Pacientes.EsFavorito(new ParaFiltroUsuarios() { IdUsuario = idMedico, IdUsuarioPaciente = idUsuario });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            EsFavorito = resultadoApi.Contenido.EsFavorito;

            MostrarEstatusFavorito = true;
        }

        private async void CargarPerfil()
        {
            var resultadoApi = await DocNocApi.Usuarios.TraeUsuarioProAPP(new ParaFiltroUsuario(){ IdUsuario = idMedico });

            if (resultadoApi.Error)
            {
                ErrorEntidad(resultadoApi);
                return;
            }

            Medico = resultadoApi;

            switch (Medico.EstatusMensaje)
            {
                case 1:
                    ChatDisponible = true;
                    ColorEstatus = (Color)Application.Current.Resources["Green"];
                    EstatusLetra = "Disponible";
                    break;
                case 3:
                    ChatDisponible = true;
                    ColorEstatus = (Color)Application.Current.Resources["Orange"];
                    EstatusLetra = "Ocupado";
                    break;
                default:
                    ChatDisponible = false;
                    ColorEstatus = (Color)Application.Current.Resources["Red"];
                    EstatusLetra = "Desconectado";
                    break;
            }

            //RefreshPage();
        }

        /*private async void RefreshPage()
        {
            
            //await Task.Delay(30000);

            CargarPerfil();
            CargarProximaCita();
        }*/

        private void AbrirChat()
        {
            Preferences.Set("IdUsuario_Chat", Medico.IdUsuario);

            //Navegación a página "Chat" (dn-71-3).
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.ChatViewModel), string.Empty, string.Empty);
        }

        private void AgendarCita()
        {
            //Navegación a página "Agendar Cita" (dn-19-3).
            Navigation.NavigateTo(typeof(ViewModels.Citas.MyAddressViewModel), string.Empty, string.Empty);
        }

        private void IrServicios()
        {
            Preferences.Set("IdMedico_Servicios", Medico.IdUsuario);

            //Navegación a página "Servicios de Médico" (dn-18-3).
            Navigation.NavigateTo(typeof(ViewModels.Medicos.ServiciosMedicoViewModel), string.Empty, string.Empty);
        }

        private void IrOpiniones()
        {
            Preferences.Set("IdMedico_Calificaciones", Medico.IdUsuario);
            Preferences.Set("NombreMedico_Calificaciones", Medico.NombreCompleto);

            //Navegación a página "Servicios de Médico" (dn-18-3).
            Navigation.NavigateTo(typeof(ViewModels.Medicos.ComentariosMedicoViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            idMedico = Preferences.Get("PerfilMedico_Id");
            CargarPerfil();
            CargarProximaCita();
            CargarEstatusFavorito();
            CargarAseguradoras();
            CargarConsultorios();
        }

        #endregion
    }
}