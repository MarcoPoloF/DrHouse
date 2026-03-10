using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using DocNoc.Xam.ViewModels.Principal;
using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PPS.Estandar;
using Syncfusion.Maui.Core;
using Syncfusion.Maui.Popup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels
{
    public class DocNocViewModel : INotifyPropertyChanged
    {
        protected static bool mockSuscriptionError = false;

        protected INavigationService Navigation;

        protected IApiService DocNocApi;

        protected IPreferenceService Preferences;

        protected IDialogService Dialog;

        protected DialogTxt DialogText;

        protected string idUsuario => Preferences.Get("IdUsuario");

        protected string IdExpediente => EsUsuarioAdicional ? Preferences.Get("idexpediente") : idUsuario;

        public BadgeIcon BadgeChats
        {
            get { return badgeChats; }
            set { SetProperty(ref badgeChats, value); }
        }
        protected BadgeIcon badgeChats;

        public BadgeIcon BadgeNotificaciones
        {
            get { return badgeNotificaciones; }
            set { SetProperty(ref badgeNotificaciones, value); }
        }
        protected BadgeIcon badgeNotificaciones;

        public string ErrorSuscripcion
        {
            get { return errorSuscripcion; }
            set { SetProperty(ref errorSuscripcion, value); }
        }
        protected string errorSuscripcion;

        public SfPopup PopupActivo
        {
            get { return popupActivo; }
            set { SetProperty(ref popupActivo, value); }
        }
        protected SfPopup popupActivo;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isBusy = false;

        public bool EsUsuarioAdicional
        {
            get { return esUsuarioAdicional; }
            set { SetProperty(ref esUsuarioAdicional, value); }
        }
        bool esUsuarioAdicional = false;

        public bool ErrorSuscripcionVisible
        {
            get { return errorSuscripcionVisible; }
            set { SetProperty(ref errorSuscripcionVisible, value); }
        }
        bool errorSuscripcionVisible = false;

        public string EnlaceSuscripcion
        {
            get { return this._enlaceSuscripcion; }
            set { SetProperty(ref _enlaceSuscripcion, value); }
        }
        private string _enlaceSuscripcion;

        public Command BackCommand { get; set; }
        public Command MisChatsCommand { get; set; }
        public Command MisNotificacionesCommand { get; set; }
        public Command PopupCommand { get; set; }
        public Command IrSuscripcionCommand { get; set; }
        

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Método que procesa el login en la app, registrando email y JWT.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="jwt">Token de acceso del usuario para la API de Doc+Noc.</param>
        protected void ProcesarLogin(string email, string jwt)
        {
            string usuario = IdUsuarioLookup(jwt);

            //Se guarda el correo del usuario en las preferencias de la App.
            Preferences.Set("email", email);
            //Se guarda el ID del usuario en las preferencias de la App.
            Preferences.Set("IdUsuario", usuario);
            //Se guarda el valor del JWT en las preferencias de la App.
            Preferences.Set("jwt", jwt);

            //Navegación a página "Principal" (dn-07-3).
            Navigation.NavigateTo(typeof(ViewModels.Principal.HomePageViewModel), string.Empty, string.Empty);
        }

        protected BadgeIcon ProcesarEstatusBadge(bool activa)
        {
            if (activa)
                return BadgeIcon.Dot;
            else
                return BadgeIcon.None;
        }

        private string IdUsuarioLookup(string jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            //Check if readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwt);

            string salida = string.Empty;

            //if (readableToken != true)
            //{
            //    txtJwtOut.Text = "The token doesn't seem to be in a proper JWT format.";
            //}
            if (readableToken == true)
            {
                var token = jwtHandler.ReadJwtToken(jwt);

                //Extract the payload of the JWT
                var claims = token.Claims;
                foreach (Claim c in claims)
                {
                    if (c.Type == "idusuario")
                        salida = c.Value;
                }
            }

            return salida;
        }

        protected void AbrirPopup(SfPopup popupLayout)
        {
            PopupActivo = popupLayout;

            PopupActivo.IsOpen = true;
        }

        protected async void CargarEstatusChats()
        {
            var respuestaApi = await DocNocApi.Mensajes.CuantosMensajesSinLeer(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                MensajeError($"No se pudo cargar el estatus de mensajes: {respuestaApi.CadenaErrores()}");
                return;
            }

            if (respuestaApi.Contenido.CuantosMensajesSinLeer > 0)
                BadgeChats = ProcesarEstatusBadge(true);
            else
                BadgeChats = ProcesarEstatusBadge(false);
        }

        protected async void CargarEstatusNotificaciones()
        {
            var respuestaApi = await DocNocApi.Mensajes.MensajeDifusionConteo(new ParaFiltroUsuario() { IdUsuario = idUsuario });

            if (respuestaApi.Error)
            {
                MensajeError($"No se pudo cargar el estatus de notificaciones: {respuestaApi.CadenaErrores()}");
                return;
            }

            if (respuestaApi.Contenido.MensajesDifusionNumero > 0)
                BadgeNotificaciones = ProcesarEstatusBadge(true);
            else
                BadgeNotificaciones = ProcesarEstatusBadge(false);
        }

        protected void Regresar()
        {
            Navigation.NavigateBack();
        }

        protected void ValidarTipoUsuario()
        {
            EsUsuarioAdicional = false;

            var tipoUsuario = Preferences.Get("TipoUsuario");

            if (tipoUsuario == "Adicional")
            {
                EsUsuarioAdicional = true;
                Preferences.Set("TipoUsuario", string.Empty);
            }
        }

        protected void CerrarSesion()
        {
            Preferences.Set("email", "");
            Preferences.Set("jwt", "");
            Navigation.NavigateTo(typeof(ViewModels.Acceso.LoginPageViewModel), string.Empty, string.Empty, true);
        }

        protected void AbrirSuscripcion()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    OpenBrowser(EnlaceSuscripcion);
                    //MensajeIOS();
                    break;
                default:
                    Navigation.NavigateTo(typeof(ViewModels.Suscripcion.MiSuscripcionViewModel), string.Empty, string.Empty);
                    break;
            }
        }

        protected async void MensajeIOS()
        {
            await Dialog.Show("Doc+Noc", "Para conocer más beneficios de Doc+Noc, te invitamos a visitar nuestro sitio web.", "Aceptar");
        }

        protected async void OpenBrowser(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        protected void MisChats()
        {
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.ConversacionesViewModel), string.Empty, string.Empty);
        }

        protected void MisNotificaciones()
        {
            Navigation.NavigateTo(typeof(ViewModels.Mensajeria.MisNotificacionesViewModel), string.Empty, string.Empty);
        }

        protected async void MensajeError(string mensaje)
        {
            await Dialog.Show("Error", mensaje, "Aceptar");
        }

        protected void ErrorCodigoRespuesta(CodigoRespuesta objeto)
        {
            if(objeto.CadenaErrores().Length > 0)
                MensajeError(objeto.CadenaErrores());

            if(objeto.Codigo != 0)
                MensajeError(objeto.MensajeCodigo);
        }

        protected void ErrorEntidad(IEntidad objeto)
        {
            MensajeError(objeto.CadenaErrores());
        }

        protected bool ValidarErrorCodigoRespuesta(CodigoRespuesta objeto)
        {
            if (objeto.Error)
                return true;

            if (objeto.Codigo != 0)
                return true;

            return false;
        }

        public async Task<PermissionStatus> ValidarPermisoImagenes()
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    if (status != PermissionStatus.Granted)
                    {
                        status = await Permissions.RequestAsync<Permissions.StorageRead>();
                    }

                    // Additionally could prompt the user to turn on in settings

                    return status;
                }

                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
                    if (status != PermissionStatus.Granted)
                    {
                        status = await Permissions.RequestAsync<Permissions.Photos>();
                    }

                    // Additionally could prompt the user to turn on in settings

                    return status;
                }

                if (DeviceInfo.Platform == DevicePlatform.UWP)
                {
                    return PermissionStatus.Granted;
                }

                return PermissionStatus.Denied;
            }
            catch
            {
                return PermissionStatus.Unknown;
            }            
        }

        protected string SerializarJson(object objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        protected T DeserializarJson<T>(string json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            //Se deserializa la respuesta al tipo de objeto solicitado.
            return JsonConvert.DeserializeObject<T>(json, jsonSettings);
        }

        public async Task<ImagenInput> CargarImagen(string nombreImagen = "DocNocApp")
        {
            //Se carga el permiso de para leer el almacenamiento.
            //var status = await ValidarPermisoImagenes();

            var respuesta = new ImagenInput();

            try
            {
                var photo = await MediaPicker.PickPhotoAsync();

                // canceled
                if (photo == null)
                {
                    return null;
                }

                var photoStream = await photo.OpenReadAsync();

                var bytes = new byte[photoStream.Length];
                await photoStream.ReadAsync(bytes, 0, (int)photoStream.Length);
                string base64 = System.Convert.ToBase64String(bytes);

                respuesta.ImagenBase64 = base64;

                respuesta.NombreImagen = $"{nombreImagen}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";

                respuesta.ExtensionImagen = Path.GetExtension(photo.FileName).Split('.')[1];

                photoStream.Dispose();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                respuesta.RegistrarError("Operación no soportada.");
            }
            catch (PermissionException pEx)
            {
                respuesta.RegistrarError("No se cuenta con permiso para acceder a las imágenes.");
            }
            catch (Exception ex)
            {
                respuesta.RegistrarError($"No se pudo completar la operación ({ex.Message}).");
            }

            return respuesta;
        }

        protected async void AbrirUri(string ruta, string descripcion)
        {
            try
            {
                var uriRuta = new Uri(ruta);

                if (await Launcher.CanOpenAsync(uriRuta))
                {
                    await Launcher.OpenAsync(uriRuta);
                }
                else
                {
                    MensajeError($"No pudo abrirse el enlace a {descripcion}.");
                }
            }
            catch (Exception ex)
            {
                MensajeError($"Excepción al abrir enlace a {descripcion}: {ex.Message}");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
