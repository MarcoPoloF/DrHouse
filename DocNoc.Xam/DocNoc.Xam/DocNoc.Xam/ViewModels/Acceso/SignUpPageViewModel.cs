using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam.ViewModels.Acceso
{
    /// <summary>
    /// Definición de ViewModel: Registro (dn-05-3)
    /// </summary>
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string firstName;

        private string lastName;

        private string password;

        private string confirmPassword;

        private RegistroTxt pageText;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Registro (dn-05-3).
            PageText = text.Get<RegistroTxt>("dn-05-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            //Se registran los comandos de la página.
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.TerminosCondicionesCommand = new Command(IrTerminosCondiciones);

            IsBusy = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName == value)
                {
                    return;
                }

                SetProperty(ref firstName, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (this.lastName == value)
                {
                    return;
                }

                SetProperty(ref lastName, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                SetProperty(ref password, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                SetProperty(ref confirmPassword, value);
            }
        }

        /// <summary>
        /// Gets or sets the text of the page.
        /// </summary>
        public RegistroTxt PageText
        {
            get { return this.pageText; }
            set { SetProperty(ref pageText, value); }
        }

        public bool WebViewVisible
        {
            get { return this._webViewVisible; }
            set { SetProperty(ref _webViewVisible, value); }
        }
        private bool _webViewVisible;

        public string WebViewTitulo
        {
            get { return this._webViewTitulo; }
            set { SetProperty(ref _webViewTitulo, value); }
        }
        private string _webViewTitulo;

        public string WebViewRuta
        {
            get { return this._webViewRuta; }
            set { SetProperty(ref _webViewRuta, value); }
        }
        private string _webViewRuta;

        public bool TermsAccepted
        {
            get { return this._termsAccepted; }
            set { SetProperty(ref _termsAccepted, value); }
        }
        private bool _termsAccepted;

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when 
        /// </summary>
        public Command TerminosCondicionesCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            //Navegación a página "Login" (dn-04-3).
            Navigation.NavigateTo(typeof(LoginPageViewModel), string.Empty, string.Empty, true);
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            IsBusy = true;

            //Se valida el nombre proporcionado.
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                await Dialog.Show(PageText.SignUpError, PageText.InvalidFirstName, DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se validan los apellidos proporcionados.
            if (string.IsNullOrWhiteSpace(LastName))
            {
                await Dialog.Show(PageText.SignUpError, PageText.InvalidLastName, DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se valida el correo electrónico proporcionado.
            if (string.IsNullOrWhiteSpace(Email))
            {
                await Dialog.Show(PageText.SignUpError, PageText.InvalidEmail, DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se valida la contraseña proporcionada.
            if (string.IsNullOrWhiteSpace(Password))
            {
                await Dialog.Show(PageText.SignUpError, PageText.InvalidPassword, DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se valida que la contraseña haya sido confirmada.
            if (string.IsNullOrWhiteSpace(ConfirmPassword) || Password != ConfirmPassword)
            {
                await Dialog.Show(PageText.SignUpError, PageText.InvalidConfirmPassword, DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se valida la acceptación de los términos y condiciones.
            if (!TermsAccepted)
            {
                await Dialog.Show(PageText.SignUpError, "Debe aceptar los términos y condiciones.", DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            var datosRegistro = new RegistroUsuario()
            {
                Nombre = FirstName,
                Apellidos = LastName,
                Email = Email,
                Contrasenia = Password,
                //Tipo de usuario
                IdTipoUsuario = 5,
                Procedencia = "Doc+Noc"
            };

            //Se envían los datos de registro a la API.
            var registroUsuario = await DocNocApi.Usuarios.Registro(datosRegistro);

            //Registro fallido.
            if (registroUsuario.Error)
            {
                await Dialog.Show(PageText.SignUpError, registroUsuario.CadenaErrores(), DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }

            //Se envían las credenciales a la API.
            var respuestaLogin = await DocNocApi.Usuarios.Acceso(new DocNoc.Models.Credenciales() { Usuario = Email, Contrasenia = Password });

            //Login fallido.
            if (respuestaLogin.Error)
            {
                await Dialog.Show(PageText.SignUpError, respuestaLogin.CadenaErrores(), DialogText.AcceptDefault);
                IsBusy = false;
                return;
            }
            //Login exitoso.
            else
            {
                //Se invoca el método que registra la información de login.
                ProcesarLogin(Email, respuestaLogin.JWT);
                IsBusy = false;
            }
        }

        private void IrTerminosCondiciones()
        {
            AbrirWebView(@"https://docnoc.mx/tyc.html", "Términos y Condiciones");
        }

        private void AbrirWebView(string ruta, string titulo)
        {
            WebViewRuta = ruta;
            WebViewTitulo = titulo;
            WebViewVisible = true;
        }

        #endregion
    }
}