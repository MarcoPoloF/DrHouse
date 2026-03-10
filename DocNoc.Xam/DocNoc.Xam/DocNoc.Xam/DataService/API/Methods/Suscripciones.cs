using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la gestión de suscripciones en Doc+Noc.
    /// </summary>
    public class Suscripciones
    {
        //Ruta base del módulo.
        private readonly string controlador = "Suscripciones";

        public async Task<ContenedorLista<SuscripcionPaciente>> ListadoSuscripcionParaPaciente()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<SuscripcionPaciente>)await api.PeticionHTTP<ContenedorLista<SuscripcionPaciente>>
                ($"{controlador}/ListadoSuscripcionParaPaciente", MetodoHTTP.Get, RequiereJwt.Si);
        }

        public async Task<ContenedorLista<MiSuscripcion>> MiSuscripcion(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<MiSuscripcion>)await api.PeticionHTTP<ContenedorLista<MiSuscripcion>>
                ($"{controlador}/MiSuscripcion", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<UsuarioSuscripcion>> TraeDatosUsuarioSuscripcion(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<UsuarioSuscripcion>)await api.PeticionHTTP<Contenedor<UsuarioSuscripcion>>
                ($"{controlador}/TraeDatosUsuarioSuscripcion", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }
    }
}
