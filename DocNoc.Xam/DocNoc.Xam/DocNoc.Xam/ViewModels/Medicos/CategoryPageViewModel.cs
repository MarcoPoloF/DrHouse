using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using DocNoc.Models;
using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Models;
using DocNoc.Xam.Models.Text;
using PPS.Estandar;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DocNoc.Xam.ViewModels.Medicos
{
    /// <summary>
    /// ViewModel for Category page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryPageViewModel : DocNocViewModel
    {
        #region Fields

        private string ciudadOCP;

        private ObservableCollection<Especialidad> categories;

        private Command categorySelectedCommand;

        private Command expandingCommand;

        private Command notificationCommand;

        private Command backButtonCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CategoryPageViewModel" /> class.
        /// </summary>
        public CategoryPageViewModel(INavigationService nav, IApiService api, ITextService text, IPreferenceService pref, IDialogService dial)
        {
            Navigation = nav;
            DocNocApi = api;
            Preferences = pref;
            Dialog = dial;

            //Carga de textos: Buscar Médico (dn-11-3).
            //PageText = text.Get<LoginTxt>("dn-11-3", pref);
            //Carga de textos: Dialog.
            DialogText = text.Get<DialogTxt>("dialog", pref);

            //Se registran los comandos de la página.
            //this.LoginCommand = new Command(this.LoginClicked);
            //this.SignUpCommand = new Command(this.SignUpClicked);
            //this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);

            CargarEspecialidades();
        }

        #endregion

        #region Public properties

        public string CiudadOCP
        {
            get { return this.ciudadOCP; }
            set
            {
                SetProperty(ref ciudadOCP, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        public ObservableCollection<Especialidad> Categories
        {
            get { return this.categories; }
            set
            {
                if (this.categories == value)
                {
                    return;
                }

                SetProperty(ref categories, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Category is selected.
        /// </summary>
        public Command CategorySelectedCommand
        {
            get { return categorySelectedCommand ?? (categorySelectedCommand = new Command(CategorySelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Notification button is clicked.
        /// </summary>
        public Command NotificationCommand
        {
            get { return notificationCommand ?? (notificationCommand = new Command(this.NotificationClicked)); }
        }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand
        {
            get { return backButtonCommand ?? (this.backButtonCommand = new Command(this.BackButtonClicked)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Category is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void CategorySelected(object obj)
        {
            if (string.IsNullOrWhiteSpace(CiudadOCP))
            {
                Dialog.Show("Error", "Especifique una ciudad o código postal.", "Aceptar");
                return;
            }

            var especialidad = (Especialidad)obj;

            Preferences.Set("BuscarMedico_CiudadOCP", ciudadOCP);
            Preferences.Set("BuscarMedico_IdEspecialidad", especialidad.IdEspecialidad.ToString());

            Navigation.NavigateTo(typeof(ViewModels.Medicos.ListaMedicosViewModel), string.Empty, string.Empty);
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void NotificationClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            // Do something
        }

        private async void CargarEspecialidades()
        {
            
        }

        #endregion
    }
}