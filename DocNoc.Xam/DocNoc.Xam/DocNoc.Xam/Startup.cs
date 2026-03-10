using DocNoc.Xam.Interfaces;
using DocNoc.Xam.ViewModels.Acceso;
using DocNoc.Xam.ViewModels.Bienvenida;
using DocNoc.Xam.ViewModels.Principal;
using System;

namespace DocNoc.Xam
{
    public class Startup
    {
        IPreferenceService preferenceService;

        public Startup(IPreferenceService preferenceService)
        {
            this.preferenceService = preferenceService;
        }

        public Type GetMainPage()
        {
            //Se valida el valor del email registrado.
            var email = preferenceService.Get("email");

            //Flujo de email vacío (sesión no iniciada).
            if (string.IsNullOrEmpty(email))
            {
                //Se devuelve el ViewModel de la página de Login (dn-04-3).
                return typeof(LoginPageViewModel);
            }
            //Flujo de email existente (sesión iniciada).
            else
            {
                //Se devuelve el ViewModel de la página principal (dn-07-3).
                return typeof(HomePageViewModel); 
            }
        }
    }
}