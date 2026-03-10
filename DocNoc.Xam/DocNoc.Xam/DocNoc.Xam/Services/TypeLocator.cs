using Autofac;
using DocNoc.API;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.ViewModels;
using System;

namespace DocNoc.Xam.Services
{
    public class TypeLocator
    {
        private IContainer container;

        private readonly ContainerBuilder containerBuilder;

        private static readonly TypeLocator TypeLocatorInstance = new TypeLocator();

        public static TypeLocator Instance
        {
            get
            {
                return TypeLocatorInstance;
            }
        }

        public TypeLocator()
        {
            containerBuilder = new ContainerBuilder(); 
            
            //Registro para Dependency Injection
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<PreferenceService>().As<IPreferenceService>();
            containerBuilder.RegisterType<TextService>().As<ITextService>();
            containerBuilder.RegisterType<ApiDataService>().As<IApiService>();

            containerBuilder.RegisterType<Startup>();
            containerBuilder.RegisterType<DocNocAPI>();

            //Registro de ViewModel: Bienvenida (dn-01-3, dn-02-3, dn-03-3)
            containerBuilder.RegisterType<ViewModels.Bienvenida.OnBoardingAnimationViewModel>();
            //Registro de ViewModel: Login (dn-04-3)
            containerBuilder.RegisterType<ViewModels.Acceso.LoginPageViewModel>();
            //Registro de ViewModel: Registro (dn-05-3)
            containerBuilder.RegisterType<ViewModels.Acceso.SignUpPageViewModel>();
            //Registro de ViewModel: Recuperar Contraseña (dn-06-3)
            containerBuilder.RegisterType<ViewModels.Acceso.ForgotPasswordViewModel>();
            //Registro de ViewModel: Home (dn-07-3)
            containerBuilder.RegisterType<ViewModels.Principal.HomePageViewModel>();
            //Registro de ViewModel: Mis Notificaciones (dn-10-3)
            containerBuilder.RegisterType<ViewModels.Mensajeria.MisNotificacionesViewModel>();
            //Registro de ViewModel: Buscar Médico (dn-11-3)
            containerBuilder.RegisterType<ViewModels.Principal.BuscarMedicoViewModel>();
            //Registro de ViewModel: Mapa de Médicos (dn-13-3)
            containerBuilder.RegisterType<ViewModels.Medicos.MapaMedicosViewModel>();
            //Registro de ViewModel: Lista de Médicos (dn-14-3)
            containerBuilder.RegisterType<ViewModels.Medicos.ListaMedicosViewModel>();
            //Registro de ViewModel: Servicios de Médico (dn-18-3)
            containerBuilder.RegisterType<ViewModels.Medicos.ServiciosMedicoViewModel>();
            //Registro de ViewModel: Agendar Cita (dn-19-3)
            containerBuilder.RegisterType<ViewModels.Citas.MyAddressViewModel>();
            //Registro de ViewModel: Detalle de Cita (dn-25-3)
            containerBuilder.RegisterType<ViewModels.Citas.DetalleCitaViewModel>();
            //Registro de ViewModel: Mis Citas (dn-26-3)
            containerBuilder.RegisterType<ViewModels.Citas.MisCitasViewModel>();
            //Registro de ViewModel: Editar Cita (dn-27-3)
            containerBuilder.RegisterType<ViewModels.Citas.EditarCitaViewModel>();
            //Registro de ViewModel: Historial de Consultas (dn-30-3)
            containerBuilder.RegisterType<ViewModels.Consultas.HistorialConsultasViewModel>();
            //Registro de ViewModel: Perfil de Médico (dn-31-3)
            containerBuilder.RegisterType<ViewModels.Medicos.ContactProfileViewModel>();
            //Registro de ViewModel: Historial de Consultas (dn-32-3)
            containerBuilder.RegisterType<ViewModels.Consultas.DetalleConsultaViewModel>();
            //Registro de ViewModel: Mis Expedientes (dn-34-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.MisExpedientesViewModel>();
            //Registro de ViewModel: Expediente (dn-36-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.ExpedienteViewModel>();
            //Registro de ViewModel: Datos Personales (dn-37-3)
            containerBuilder.RegisterType<ViewModels.Usuarios.DatosPersonalesViewModel>();
            //Registro de ViewModel: Alergias (dn-38-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.AlergiasViewModel>();
            //Registro de ViewModel: Tipo de Sangre (dn-40-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.TipoSangreViewModel>();
            //Registro de ViewModel: Padecimientos Actuales (dn-41-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.PadecimientosActualesViewModel>();
            //Registro de ViewModel: Antecedentes Familiares (dn-43-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.AntecedentesFamiliaresViewModel>();
            //Registro de ViewModel: Historia Clínica (dn-45-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.HistoriaClinicaViewModel>();
            //Registro de ViewModel: Estudios y Análisis (dn-47-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.EstudiosAnalisisViewModel>();
            //Registro de ViewModel: Medicamentos (dn-49-3)
            containerBuilder.RegisterType<ViewModels.Expedientes.MedicamentosViewModel>();
            //Registro de ViewModel: Favoritos (dn-56-3)
            containerBuilder.RegisterType<ViewModels.Principal.FavoritosViewModel>();
            //Registro de ViewModel: Mi Perfil (dn-59-3)
            containerBuilder.RegisterType<ViewModels.Principal.MiPerfilViewModel>();
            //Registro de ViewModel: Datos de Facturación (dn-60-3)
            containerBuilder.RegisterType<ViewModels.Usuarios.DatosFacturacionViewModel>();
            //Registro de ViewModel: Cambiar Contraseña (dn-61-3)
            containerBuilder.RegisterType<ViewModels.Usuarios.CambiarPasswordViewModel>();
            //Registro de ViewModel: Formas de Pago (dn-62-3)
            containerBuilder.RegisterType<ViewModels.Suscripcion.FormasPagoViewModel>();
            //Registro de ViewModel: Mi Suscripción (dn-64-3)
            containerBuilder.RegisterType<ViewModels.Suscripcion.MiSuscripcionViewModel>();
            //Registro de ViewModel: Configuración de Notificaciones (dn-68-3)
            containerBuilder.RegisterType<ViewModels.Mensajeria.ConfigNotificacionesViewModel>();
            //Registro de ViewModel: Chat (dn-71-3)
            containerBuilder.RegisterType<ViewModels.Mensajeria.ChatViewModel>();
            //Registro de ViewModel: Acerca De (dn-XX-3)
            containerBuilder.RegisterType<ViewModels.Principal.AcercaDeViewModel>();
            //Registro de ViewModel: Conversaciones (dn-XX-3)
            containerBuilder.RegisterType<ViewModels.Mensajeria.ConversacionesViewModel>();
            //Registro de ViewModel: Calificaciones de Médicos (dn-XX-3)
            containerBuilder.RegisterType<ViewModels.Medicos.ComentariosMedicoViewModel>();
        }

        public object Resolve(Type type, NamedParameter namedParameter = null)
        {
            if (namedParameter == null)
            {
                return container.Resolve(type);
            }
            return container.Resolve(type, namedParameter);
        }

        public void Build()
        {
            container = containerBuilder?.Build();
        }
    }
}
