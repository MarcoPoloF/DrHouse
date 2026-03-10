using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenPay
    {
        //Ruta base del módulo.
        private readonly string controlador = "gCjcihB2927xKVuq";

        public async Task<CodigoRespuesta> AltaTarjeta(AltaTarjetaParaPaciente content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/jNs9eePNHVtJbsnP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<CodigoRespuesta> ComprarSuscripcion(CompraSuscripcionPaciente content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (CodigoRespuesta)await api.PeticionHTTP<CodigoRespuesta>
                ($"{controlador}/MTTazNk6q9pddkBg", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ValidacionSuscripcion> PreValidacionPaciente(ParaFiltroUsuarioyDato content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ValidacionSuscripcion)await api.PeticionHTTP<ValidacionSuscripcion>
                ($"{controlador}/PreValidacionPaciente", MetodoHTTP.Post, RequiereJwt.Si, content);
        }
    }
}
