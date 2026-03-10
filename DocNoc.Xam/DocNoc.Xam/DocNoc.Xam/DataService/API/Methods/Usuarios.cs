using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la información de los usuarios de Doc+Noc.
    /// </summary>
    public class Usuarios
    {
        //Ruta base del módulo.
        private readonly string controlador = "Usuarios";

        /// <summary>
        /// Valida las credenciales de acceso de un usuario de Doc+Noc.
        /// </summary>
        /// <param name="content">Las credenciales del usuario.</param>
        /// <returns>La información del usuario, incluyendo su Json Web Token de autenticación.</returns>
        public async Task<JsonWebToken> Acceso(Credenciales content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (JsonWebToken) await api.PeticionHTTP<JsonWebToken>
                ($"{controlador}/AccesoAPP", MetodoHTTP.Post, RequiereJwt.No, content);
        }

        public async Task<CodigoRespuesta> ActualizaFotoPerfil(Usuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaFotoPerfil", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ActualizaFotoPerfilAdicional(UsuarioAdicionalAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaFotoPerfilAdicional", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ActualizaPerfil(Usuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaPerfil", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ActualizaUsuarioAdicional(UsuarioAdicionalAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaUsuarioAdicional", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ActualizaTipodeSangre(TipoDeSangre content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ActualizaTipodeSangre", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> AgregaUsuarioAdicional(UsuarioAdicional content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/AgregaUsuarioAdicional", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> CambiaContrasenia(NuevaContrasenia content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/CambiaContrasenia", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> EliminaUsuarioAdicional(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/EliminaUsuarioAdicional", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> InsertaUsuarioAlergia(UsuarioAlergia content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaUsuarioAlergia", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> InsertaUsuarioAntecedenteFamiliarPatologico(UsuarioAntecedenteFamiliarPatologico content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaUsuarioAntecedenteFamiliarPatologico", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> InsertaUsuarioHistoriaClinica(UsuarioHistoriaClinica content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaUsuarioHistoriaClinica", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> InsertaUsuarioPadecimiento(UsuarioPadecimiento content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/InsertaUsuarioPadecimiento", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<Anuncio>> ListadoAnunciosPacientes()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<Anuncio>)await api.PeticionHTTP<ContenedorLista<Anuncio>>
                ($"Anuncios/ListadoAnunciosPacientes", MetodoHTTP.Get, RequiereJwt.Si);
        }

        public async Task<ContenedorLista<UsuarioServicioTratamiento>> ListaUsuarioServicioTratamiento(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioServicioTratamiento>)await api.PeticionHTTP<ContenedorLista<UsuarioServicioTratamiento>>
                ($"{controlador}/ListaUsuarioServicioTratamiento", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        /// <summary>
        /// Solicita la regeneración de contraseña de un usuario de Doc+Noc, la cual se envía por correo electrónico.
        /// </summary>
        /// <param name="content">Correo electrónico del usuario.</param>
        /// <returns>Objeto Entidad que indica el resultado de la solicitud.</returns>
        public async Task<Entidad> RecuperarContrasenia(NuevoEmail content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Entidad) await api.PeticionHTTP<Entidad>
                ($"{controlador}/RecuperarContrasenia", MetodoHTTP.Post, RequiereJwt.No, content);
        }

        /// <summary>
        /// Ejecuta el registro de un nuevo usuario en Doc+Noc.
        /// </summary>
        /// <param name="content">Los datos esenciales del usuario a registrar.</param>
        /// <returns>Código de respuesta indicando el resultado de la solicitud.</returns>
        public async Task<CodigoRespuesta> Registro(RegistroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta) await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/Registro", MetodoHTTP.Post, RequiereJwt.No, content);
        }

        public async Task<Contenedor<Usuario>> TraeUsuario(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<Usuario>)await api.PeticionHTTP<Contenedor<Usuario>>
                ($"{controlador}/TraeUsuario", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<Contenedor<UsuarioAdicionalAPP>> TraeUsuarioAdicional(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<UsuarioAdicionalAPP>)await api.PeticionHTTP<Contenedor<UsuarioAdicionalAPP>>
                ($"{controlador}/TraeUsuarioAdicional", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<ContenedorLista<UsuarioAseguradora>> TraeUsuarioAseguradora(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioAseguradora>)await api.PeticionHTTP<ContenedorLista<UsuarioAseguradora>>
                ($"{controlador}/TraeUsuarioAseguradora", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<TipoDeSangre>> TraeTipodeSangre(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<TipoDeSangre>)await api.PeticionHTTP<ContenedorLista<TipoDeSangre>>
                ($"{controlador}/TraeTipodeSangre", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<UsuarioAlergia>> TraeUsuarioAlergia(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioAlergia>)await api.PeticionHTTP<ContenedorLista<UsuarioAlergia>>
                ($"{controlador}/TraeUsuarioAlergia", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<UsuarioAntecedenteFamiliarPatologico>> TraeUsuarioAntecedenteFamiliarPatologico(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioAntecedenteFamiliarPatologico>)await api.PeticionHTTP<ContenedorLista<UsuarioAntecedenteFamiliarPatologico>>
                ($"{controlador}/TraeUsuarioAntecedenteFamiliarPatologico", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<UsuarioHistoriaClinica2>> TraeUsuarioHistoriaClinica(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioHistoriaClinica2>)await api.PeticionHTTP<ContenedorLista<UsuarioHistoriaClinica2>>
                ($"{controlador}/TraeUsuarioHistoriaClinica", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<ProximaCitaMedico>> TraeUsuarioProAPPProximaCita(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<ProximaCitaMedico>)await api.PeticionHTTP<Contenedor<ProximaCitaMedico>>
                ($"{controlador}/TraeUsuarioProAPPProximaCita", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

        public async Task<ContenedorLista<Consulta>> TraeHistoriaConsulta(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<Consulta>)await api.PeticionHTTP<ContenedorLista<Consulta>>
                ($"{controlador}/TraeHistoriaConsulta", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<DetalleConsulta>> TraeHistoriaConsultaDetalleAPP(int IdConsulta)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<DetalleConsulta>)await api.PeticionHTTP<Contenedor<DetalleConsulta>>
                ($"{controlador}/TraeHistoriaConsultaDetalleAPP/{IdConsulta}", MetodoHTTP.Get, RequiereJwt.Si, null, true);
        }

        public async Task<ContenedorLista<UsuarioPadecimiento>> TraeUsuarioPadecimiento(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioPadecimiento>)await api.PeticionHTTP<ContenedorLista<UsuarioPadecimiento>>
                ($"{controlador}/TraeUsuarioPadecimiento", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<UsuarioPago>> TraeUsuarioPago(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioPago>)await api.PeticionHTTP<ContenedorLista<UsuarioPago>>
                ($"{controlador}/TraeUsuarioPago", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<UsuarioAPP> TraeUsuarioProAPP(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (UsuarioAPP)await api.PeticionHTTP<UsuarioAPP>
                ($"{controlador}/TraeUsuarioProAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<UsuarioSetNotificacion>> TraeUsuarioSetNotificacion(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<UsuarioSetNotificacion>)await api.PeticionHTTP<ContenedorLista<UsuarioSetNotificacion>>
                ($"{controlador}/TraeUsuarioSetNotificacion", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ModificaUsuarioSetNotificacionAppWeb (UsuarioSetNotificacion content)
        {
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/ModificaUsuarioSetNotificacionAppWeb", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<Contenedor<Usuario>> cW4UZsWHdz4bYWF2(ParaFiltroUsuarioyDato content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (Contenedor<Usuario>)await api.PeticionHTTP<Contenedor<Usuario>>
                ($"{controlador}/cW4UZsWHdz4bYWF2", MetodoHTTP.Post, RequiereJwt.Si, content, true);
        }

    }
}
