using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con el manejo de mensajes y notificaciones de Doc+Noc.
    /// </summary>
    public class Mensajes
    {
        //Ruta base del módulo.
        private readonly string controlador = "Mensajes";

        public async Task<CodigoRespuesta> AvisaLlegada(ParaAvisaLlegada content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/AvisaLlegada", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<EstatusChats>> CuantosMensajesSinLeer(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (Contenedor<EstatusChats>)await api.PeticionHTTP<Contenedor<EstatusChats>>
                ($"{controlador}/CuantosMensajesSinLeer", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<CodigoRespuesta> EnviarMensajeUnico(ParaEnvioMensajeUnico content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/EnviarMensajeUnico", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ParaLeeConversacion>> LeeConversacionAPP(ParaFiltroLeeConversacionAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ParaLeeConversacion>)await api.PeticionHTTP<ContenedorLista<ParaLeeConversacion>>
                ($"{controlador}/LeeConversacionAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> EstableceMensajesEnLeido (ParaFiltroUsuarios content)
        {
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/EstableceMensajesEnLeido", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ParaLeeConversacion>> LeeConversacionAPP1(ParaFiltroLeeConversacionAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ParaLeeConversacion>)await api.PeticionHTTP<ContenedorLista<ParaLeeConversacion>>
                ($"{controlador}/LeeConversacionAPP1", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ParaLeeConversacion>> LeeConversacionAPP2(ParaFiltroLeeConversacionAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ParaLeeConversacion>)await api.PeticionHTTP<ContenedorLista<ParaLeeConversacion>>
                ($"{controlador}/LeeConversacionAPP2", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ListadoMensaje>> ListadoNotificaciones()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ListadoMensaje>)await api.PeticionHTTP<ContenedorLista<ListadoMensaje>>($"{controlador}/ListadoNotificaciones", MetodoHTTP.Post, RequiereJwt.Si);
        }

        public async Task<Contenedor<EstatusNotificaciones>> MensajeDifusionConteo(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (Contenedor<EstatusNotificaciones>)await api.PeticionHTTP<Contenedor<EstatusNotificaciones>>
                ($"{controlador}/MensajeDifusionConteo", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<ContenedorLista<MensajeDifusionTexto>> MensajeDifusionTexto(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<MensajeDifusionTexto>)await api.PeticionHTTP<ContenedorLista<MensajeDifusionTexto>>
                ($"{controlador}/MensajeDifusionTexto", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ParaPaginaMensajeAPP>> PaginaMensajeAPP(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<ParaPaginaMensajeAPP>)await api.PeticionHTTP<ContenedorLista<ParaPaginaMensajeAPP>>
                ($"{controlador}/PaginaMensajeAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> EstableceEliminado(int idMensaje)
        {
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/EstableceEliminado/{idMensaje}", MetodoHTTP.Post, RequiereJwt.Si);
        }
    }
}
