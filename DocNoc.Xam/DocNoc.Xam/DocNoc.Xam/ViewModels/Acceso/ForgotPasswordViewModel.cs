using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Acceso
{
    /// <summary>
    /// Definición de ViewModel: Recuperar Contraseña (dn-06-3)
    /// </summary>
    public class ForgotPasswordViewModel : LoginViewModel
    {
        #region Fields

        private RecuperarContrasenaTxt pageText;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordViewModel" /> class.
        /// </summary>
        public ForgotPasswordViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Recuperar Contraseña (dn-06-3).
            PageText = text.Get<RecuperarContrasenaTxt>("dn-06-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            //Se registran los comandos de la página.
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.SendCommand = new Command(this.SendClicked);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the page.
        /// </summary>
        public RecuperarContrasenaTxt PageText
        {
            get { return this.pageText; }
            set { SetProperty(ref pageText, value); }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SendClicked(object obj)
        {
            //Se valida el correo electrónico proporcionado.
            if (string.IsNullOrWhiteSpace(Email))
            {
                await Dialog.Show(PageText.RecoverPasswordError, PageText.EmailInvalid, DialogText.AcceptDefault);
                return;
            }

            //Se envía el correo electrónico a la API.
            var recuperarContrasena = await DocNocApi.Usuarios.RecuperarContrasenia(new NuevoEmail() { Email = Email });

            //Recuperación de contraseña fallida.
            if (recuperarContrasena.Error)
            {
                await Dialog.Show(PageText.RecoverPasswordError, recuperarContrasena.CadenaErrores(), DialogText.AcceptDefault);
                return;
            }

            //Recuperación exitosa de contraseña.
            await Dialog.Show(PageText.RecoverPasswordSuccess, recuperarContrasena.CadenaMensajes(), DialogText.AcceptDefault);
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            //Navegación a página "Registro" (dn-05-3).
            Navigation.NavigateTo(typeof(SignUpPageViewModel), string.Empty, string.Empty);
        }

        #endregion
    }
}