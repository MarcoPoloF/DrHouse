using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con citas programadas mediante Doc+Noc.
    /// </summary>
    public class Citas
    {
        //Ruta base del módulo.
        private readonly string controlador = "Cita";

        public async Task<ContenedorLista<ResultadoBusquedaMedicoAPP>> BuscaMedico2(BusquedaMedicoAPP content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ResultadoBusquedaMedicoAPP>)await api.PeticionHTTP<ContenedorLista<ResultadoBusquedaMedicoAPP>>
                ($"{controlador}/BuscaMedico2", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ResultadoBusquedaMedicoAPP2>> BuscaMedico3()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ResultadoBusquedaMedicoAPP2>)await api.PeticionHTTP<ContenedorLista<ResultadoBusquedaMedicoAPP2>>
                ($"{controlador}/BuscaMedico3", MetodoHTTP.Get, RequiereJwt.Si);
        }

        public async Task<CodigoRespuesta> CalificaCitaalDoctor(CalificaCitaConComentario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/CalificaCitaalDoctor", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> CancelaCitaPaciente(int idCita)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/CancelaCitaPaciente/{idCita}", MetodoHTTP.Post, RequiereJwt.Si);
        }

        public async Task<ContenedorLista<CitasProximas>> CitaProxima(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<CitasProximas>)await api.PeticionHTTP<ContenedorLista<CitasProximas>>
                ($"{controlador}/CitaProxima", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> CreaCita(ParaCrearCita content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/CreaCita", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ResultadoMiCita>> MiCita(int idCita)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ResultadoMiCita>)await api.PeticionHTTP<ContenedorLista<ResultadoMiCita>>
                ($"{controlador}/MiCita/{idCita}", MetodoHTTP.Get, RequiereJwt.Si, true);
        }

        public async Task<ContenedorLista<ResultadoMisCitas>> MisCitas(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ResultadoMisCitas>)await api.PeticionHTTP<ContenedorLista<ResultadoMisCitas>>
                ($"{controlador}/MisCitas", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> PosponeCitaPaciente(ParaPosponerCita content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/PosponeCitaPaciente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<CitaConsultorio>> TraeAgendaMedicoAPP(ParaFiltroUsuarioConsultorio content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<CitaConsultorio>)await api.PeticionHTTP<ContenedorLista<CitaConsultorio>>
                ($"{controlador}/TraeAgendaMedicoAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ParaTraeReviewsDelDoctor>> TraeReviewsDelDoctor(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ParaTraeReviewsDelDoctor>)await api.PeticionHTTP<ContenedorLista<ParaTraeReviewsDelDoctor>>
                ($"{controlador}/TraeReviewsDelDoctor", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<ResultadoUsuarioAdicional>> TraeUsuarioAdicional(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<ResultadoUsuarioAdicional>)await api.PeticionHTTP<ContenedorLista<ResultadoUsuarioAdicional>>
                ($"{controlador}/TraeUsuarioAdicional", MetodoHTTP.Post, RequiereJwt.Si, content);
        }
    }
}
