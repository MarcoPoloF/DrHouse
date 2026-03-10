using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Acceso
{
    /// <summary>
    /// Definición de ViewModel: Login (dn-04-3).
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;

        private LoginTxt pageText;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Login (dn-04-3).
            PageText = text.Get<LoginTxt>("dn-04-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            //Se registran los comandos de la página.
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);

            IsBusy = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
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
        /// Gets or sets the page text.
        /// </summary>
        public LoginTxt PageText
        {
            get { return this.pageText; }
            set { SetProperty(ref pageText, value); }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        //public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            IsBusy = true;

            //Se envían las credenciales a la API.
            var respuesta = await DocNocApi.Usuarios.Acceso(new DocNoc.Models.Credenciales() { Usuario = Email, Contrasenia = Password });

            //Login fallido.
            if(respuesta.Error)
            {
                await Dialog.Show(PageText.LoginError, respuesta.CadenaErrores(), DialogText.AcceptDefault);
            }
            //Login exitoso.
            else
            {
                //Se invoca el método que registra la información de login.
                ProcesarLogin(Email, respuesta.JWT);                
            }

            IsBusy = false;
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

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            //Navegación a página "Recuperar Contraseña" (dn-06-3).
            Navigation.NavigateTo(typeof(ForgotPasswordViewModel), string.Empty, string.Empty);
        }

        public void OnAppearing()
        {
            //Se carga la preferenica PrimerUso, que indica si la aplicación ha sido abierta en el dispositivo.
            var primeraEjecucion = Preferences.Get("primeraejecucion");

            //Flujo de primera ejecución.
            if (primeraEjecucion != "false")
            {
                //Se desactiva la bandera de primer uso.
                Preferences.Set("primeraejecucion", "false");

                //Navegación a página "Bienvenida" (dn-01-3).
                Navigation.NavigateTo(typeof(ViewModels.Bienvenida.OnBoardingAnimationViewModel), string.Empty, string.Empty, true);
            }
        }

        #endregion
    }
}