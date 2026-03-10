using DocNoc.Xam.Interfaces;
using DocNoc.Xam.Services;
using DocNoc.Xam.ViewModels;
using DocNoc.Xam.Views.Acceso;
using DocNoc.Xam.Views.ErrorAndEmpty;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace DocNoc.Xam
{
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand => new Command(GoToLogout);
        NavigationService navigationService = (NavigationService)TypeLocator.Instance.Resolve(typeof(INavigationService));

        public AppShell()
        {
            TabBar barra = new TabBar();

            ShellContent content = new ShellContent();

            InitializeComponent();

            //Home
            content.Title = "Home";
            content.Icon = ImageSource.FromFile("home.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.HomePageViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Buscar
            content = new ShellContent();
            content.Title = "Buscar";
            content.Icon = ImageSource.FromFile("search.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.BuscarMedicoViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Mis Citas
            content = new ShellContent();
            content.Title = "Citas";
            content.Icon = ImageSource.FromFile("appointments.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Citas.MisCitasViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Favoritos
            content = new ShellContent();
            content.Title = "Favoritos";
            content.Icon = ImageSource.FromFile("favorites.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.FavoritosViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Mi Perfil
            content = new ShellContent();
            content.Title = "Perfil";
            content.Icon = ImageSource.FromFile("profile.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.MiPerfilViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            this.Items.Add(barra);

            RegisterRoutes();
        }

        internal static Page Init()
        {
            IntializeBuildContainer();

            var navigationService = TypeLocator.Instance.Resolve(typeof(INavigationService)) as INavigationService;
            var startup = TypeLocator.Instance.Resolve(typeof(Startup)) as Startup;

            var mainPage = startup!.GetMainPage();

            if (mainPage == typeof(ViewModels.Principal.HomePageViewModel))
                return new AppShell();

            var paginaLogin = ((NavigationService)navigationService!).GetPageWithBindingContext(mainPage, string.Empty, string.Empty);

            return new NavigationPage(paginaLogin);
        }

        private void RegisterRoutes()
        {
            //-- Registro de Rutas: Acceso --//
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("reiniciarcontrasena", typeof(SimpleForgotPasswordPage));
            Routing.RegisterRoute("registro", typeof(SimpleSignUpPage));
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        void GoToLogout()
        {
            Preferences.Set("email", "");
            Preferences.Set("jwt", "");
            navigationService.NavigateTo(typeof(ViewModels.Acceso.LoginPageViewModel), string.Empty, string.Empty, true);
        }

        private static void IntializeBuildContainer()
        {
            TypeLocator.Instance.Build();
        }

        private static void ListenNetworkChanges()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private static void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            CheckInternet();
        }

        static bool onErrorPage;
        private static void CheckInternet()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                onErrorPage = true;
                Application.Current!.MainPage!.Navigation.PushAsync(new NoInternetConnectionPage());
            }
            else if (onErrorPage)
            {
                Application.Current!.MainPage!.Navigation.PopAsync();
                onErrorPage = false;
            }
        }
    }
}

        public ICommand LogoutCommand => new Command(GoToLogout);
        NavigationService navigationService = (NavigationService)TypeLocator.Instance.Resolve(typeof(INavigationService));

        public AppShell()
        {
            TabBar barra = new TabBar();

            ShellContent content = new ShellContent();

            InitializeComponent();

            //Home
            content.Title = "Home";
            content.Icon = ImageSource.FromFile("home.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.HomePageViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Buscar
            content = new ShellContent();
            content.Title = "Buscar";
            content.Icon = ImageSource.FromFile("search.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.BuscarMedicoViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Mis Citas
            content = new ShellContent();
            content.Title = "Citas";
            content.Icon = ImageSource.FromFile("appointments.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Citas.MisCitasViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Favoritos
            content = new ShellContent();
            content.Title = "Favoritos";
            content.Icon = ImageSource.FromFile("favorites.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.FavoritosViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            //Mi Perfil
            content = new ShellContent();
            content.Title = "Perfil";
            content.Icon = ImageSource.FromFile("profile.png");
            content.Content = navigationService.GetPageWithBindingContext(typeof(ViewModels.Principal.MiPerfilViewModel), string.Empty, string.Empty);
            barra.Items.Add(content);

            this.Items.Add(barra);

            //FlyoutItem item = new FlyoutItem() { Title = "Home" };
            //ShellContent content = new ShellContent();

            ////Home Page
            //content.Title = "Home";
            //content.Content = navigationService.GetPageWithBindingContext(typeof(HomePageViewModel), string.Empty, string.Empty);
            //item.Items.Add(content);
            //this.Items.Add(item);

            ////Buscar Page
            //item = new FlyoutItem() { Title = "Buscar" };
            //content = new ShellContent();
            //content.Title = "Buscar";
            //content.Content = navigationService.GetPageWithBindingContext(typeof(HomePageViewModel), string.Empty, string.Empty);
            //item.Items.Add(content);
            //this.Items.Add(item);

            ////Aboutus Page
            //item = new FlyoutItem() { Title = "Acerca de" };
            //content = new ShellContent();
            //content.Title = "About";
            //content.Content = navigationService.GetPageWithBindingContext(typeof(AboutUsViewModel), string.Empty, string.Empty);
            //item.Items.Add(content);
            ////this.Items.Add(item);

            ////Logout Menu
            //MenuItem logout = new MenuItem() { Text = "Logout", Command = new Command(GoToLogout) };
            //this.Items.Add(logout);

            RegisterRoutes();
        }

        internal static Page Init()
        {
            //ListenNetworkChanges();
            IntializeBuildContainer();

            var navigationService = TypeLocator.Instance.Resolve(typeof(INavigationService)) as INavigationService;
            var startup = TypeLocator.Instance.Resolve(typeof(Startup)) as Startup;

            var mainPage = startup.GetMainPage();

            if (mainPage == typeof(ViewModels.Principal.HomePageViewModel))
                return new AppShell();

            var paginaLogin = ((NavigationService)navigationService).GetPageWithBindingContext(mainPage, string.Empty, string.Empty);

            return new NavigationPage(paginaLogin);
        }

        private void RegisterRoutes()
        {
            //-- Registro de Rutas: Acceso --//
            //Registro de página de Login.
            Routing.RegisterRoute("login", typeof(LoginPage));
            //Registro de página de Reiniciar Contraseña.
            Routing.RegisterRoute("reiniciarcontrasena", typeof(SimpleForgotPasswordPage));
            //Registro de página de Registro.
            Routing.RegisterRoute("registro", typeof(SimpleSignUpPage));
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        void GoToLogout()
        {
            Preferences.Set("email", "");
            Preferences.Set("jwt", "");
            navigationService.NavigateTo(typeof(ViewModels.Acceso.LoginPageViewModel), string.Empty, string.Empty, true);
        }

        private static void IntializeBuildContainer()
        {
            TypeLocator.Instance.Build();
        }

        private static void ListenNetworkChanges()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckInternet();
        }

        static bool onErrorPage;
        private static void CheckInternet()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                onErrorPage = true;
                Application.Current.MainPage.Navigation.PushAsync(new NoInternetConnectionPage());
            }
            else if (onErrorPage)
            {
                Application.Current.MainPage.Navigation.PopAsync();
                onErrorPage = false;
            }
        }
    }
}