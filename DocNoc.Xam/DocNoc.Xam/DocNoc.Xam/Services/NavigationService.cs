using DocNoc.Xam.Interfaces;
using DocNoc.Xam.ViewModels;
using DocNoc.Xam.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DocNoc.Xam.Services
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> MappingPageAndViewModel;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {
            MappingPageAndViewModel = new Dictionary<Type, Type>();

            SetPageViewModelMappings();
        }

        public async void NavigateBack()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync(true);
        }

        public async void NavigateTo(Type type, string parameterName, string parameterValue, bool replaceView = false)
        {
            if(type == typeof(ViewModels.Principal.HomePageViewModel) && string.IsNullOrEmpty(parameterValue))
            {
                CurrentApplication.MainPage = new AppShell();
                return;
            }
            if (!replaceView)
            {
                await CurrentApplication.MainPage.Navigation.PushAsync(GetPageWithBindingContext(type, parameterName, parameterValue), true);
            }
            else
            {
                CurrentApplication.MainPage = new NavigationPage(GetPageWithBindingContext(type, parameterName, parameterValue));
            }
        }

        /// <summary>
        /// Método en el que se mapean las relaciones entre ViewModels y Views.
        /// </summary>
        private void SetPageViewModelMappings()
        {
            //Mapeo de página: Bienvenida (dn-01-3, dn-02-3, dn-03-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Bienvenida.OnBoardingAnimationViewModel), typeof(Views.Bienvenida.OnBoardingAnimationPage));
            //Mapeo de página: Login (dn-04-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Acceso.LoginPageViewModel), typeof(Views.Acceso.LoginPage));
            //Mapeo de página: Registro (dn-05-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Acceso.SignUpPageViewModel), typeof(Views.Acceso.SimpleSignUpPage));
            //Mapeo de página: Recuperar Contraseña (dn-06-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Acceso.ForgotPasswordViewModel), typeof(Views.Acceso.SimpleForgotPasswordPage));
            //Mapeo de página: Principal (dn-07-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Principal.HomePageViewModel), typeof(Views.Principal.HomePage));
            //Mapeo de página: Mis Notificaciones (dn-10-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Mensajeria.MisNotificacionesViewModel), typeof(Views.Mensajeria.MisNotificacionesView));
            //Mapeo de página: Buscar Médico (dn-11-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Principal.BuscarMedicoViewModel), typeof(Views.Principal.BuscarMedicoView));
            //Mapeo de página: Mapa de Médicos (dn-13-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Medicos.MapaMedicosViewModel), typeof(Views.Medicos.MapaMedicosView));
            //Mapeo de página: Lista de Médicos (dn-14-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Medicos.ListaMedicosViewModel), typeof(Views.Medicos.ListaMedicosView));
            //Mapeo de página: Servicios de Médico (dn-18-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Medicos.ServiciosMedicoViewModel), typeof(Views.Medicos.ServiciosMedicoView));
            //Mapeo de página: Agendar Cita (dn-19-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Citas.MyAddressViewModel), typeof(Views.Citas.MyAddressPage));
            //Mapeo de página: Detalle de Cita (dn-25-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Citas.DetalleCitaViewModel), typeof(Views.Citas.DetalleCitaView));
            //Mapeo de página: Mis Citas (dn-26-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Citas.MisCitasViewModel), typeof(Views.Citas.MisCitas));
            //Mapeo de página: Editar Cita (dn-27-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Citas.EditarCitaViewModel), typeof(Views.Citas.EditarCitaView));
            //Mapeo de página: Historial de Consultas (dn-30-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Consultas.HistorialConsultasViewModel), typeof(Views.Consultas.HistorialConsultasView));
            //Mapeo de página: Perfil de Médico (dn-31-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Medicos.ContactProfileViewModel), typeof(Views.Medicos.ContactProfilePage));
            //Mapeo de página: Detalle de Consulta (dn-32-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Consultas.DetalleConsultaViewModel), typeof(Views.Consultas.DetalleConsultaView));
            //Mapeo de página: Mis Expedientes (dn-34-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.MisExpedientesViewModel), typeof(Views.Expedientes.MisExpedientesView));
            //Mapeo de página: Expediente (dn-36-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.ExpedienteViewModel), typeof(Views.Expedientes.ExpedienteView));
            //Mapeo de página: Datos Personales (dn-37-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Usuarios.DatosPersonalesViewModel), typeof(Views.Usuarios.DatosPersonalesView));
            //Mapeo de página: Alergias (dn-38-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.AlergiasViewModel), typeof(Views.Expedientes.AlergiasView));
            //Mapeo de página: Tipo de Sangre (dn-40-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.TipoSangreViewModel), typeof(Views.Expedientes.TipoSangreView));
            //Mapeo de página: Padecimientos Actuales (dn-41-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.PadecimientosActualesViewModel), typeof(Views.Expedientes.PadecimientosActualesView));
            //Mapeo de página: Antecedentes Familiares (dn-43-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.AntecedentesFamiliaresViewModel), typeof(Views.Expedientes.AntecedentesFamiliaresView));
            //Mapeo de página: Historia Clínica (dn-45-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.HistoriaClinicaViewModel), typeof(Views.Expedientes.HistoriaClinicaView));
            //Mapeo de página: Estudios y Análisis (dn-47-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.EstudiosAnalisisViewModel), typeof(Views.Expedientes.EstudiosAnalisisView));
            //Mapeo de página: Medicamentos (dn-49-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Expedientes.MedicamentosViewModel), typeof(Views.Expedientes.MedicamentosView));
            //Mapeo de página: Favoritos (dn-56-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Principal.FavoritosViewModel), typeof(Views.Principal.FavoritosView));
            //Mapeo de página: Mi Perfil (dn-59-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Principal.MiPerfilViewModel), typeof(Views.Principal.MiPerfilView));
            //Mapeo de página: Datos de Facturación (dn-60-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Usuarios.DatosFacturacionViewModel), typeof(Views.Usuarios.DatosFacturacionView));
            //Mapeo de página: Cambiar Contraseña (dn-61-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Usuarios.CambiarPasswordViewModel), typeof(Views.Usuarios.CambiarPasswordView));
            //Mapeo de página: Formas de Pago (dn-62-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Suscripcion.FormasPagoViewModel), typeof(Views.Suscripcion.FormasPagoView));
            //Mapeo de página: Suscripción (dn-64-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Suscripcion.MiSuscripcionViewModel), typeof(Views.Suscripcion.MiSuscripcionView));
            //Mapeo de página: Configuración de Notificaciones (dn-68-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Mensajeria.ConfigNotificacionesViewModel), typeof(Views.Mensajeria.ConfigNotificacionesView));
            //Mapeo de página: Chat (dn-71-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Mensajeria.ChatViewModel), typeof(Views.Mensajeria.ChatView));
            //Mapeo de página: AcercaDe (dn-XX-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Principal.AcercaDeViewModel), typeof(Views.Principal.AcercaDeView));
            //Mapeo de página: Conversaciones (dn-XX-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Mensajeria.ConversacionesViewModel), typeof(Views.Mensajeria.ConversacionesView));
            //Mapeo de página: Opiniones de Médico (dn-XX-3)
            MappingPageAndViewModel.Add(typeof(ViewModels.Medicos.ComentariosMedicoViewModel), typeof(Views.Medicos.ComentariosMedicoView));
        }

        public Page GetPageWithBindingContext(Type type, string parameterName, string parameterValue)
        {
            Type pageType = GetPageForViewModel(type);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {type} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;

            if (string.IsNullOrEmpty(parameterName))
            {
                page.BindingContext = TypeLocator.Instance.Resolve(type) as DocNocViewModel;
            }
            else
            {
                page.BindingContext = TypeLocator.Instance.Resolve(type, new Autofac.NamedParameter(parameterName, parameterValue)) as DocNocViewModel;
            }

            return page;
        }

        /// <summary>
        /// Método empleado para obtener la vista asociada a un ViewModel específico.
        /// </summary>
        /// <param name="viewModelType">La clase del ViewModel.</param>
        /// <returns>La clase de la View asociado al ViewModel proporcionado.</returns>
        private Type GetPageForViewModel(Type viewModelType)
        {
            if (!MappingPageAndViewModel.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return MappingPageAndViewModel[viewModelType];
        }
    }
}