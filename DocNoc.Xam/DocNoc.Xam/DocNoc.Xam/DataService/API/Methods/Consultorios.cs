using DocNoc.Models;
using DocNoc.Xam.Models.Consultorios;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la información de aseguradoras registradas en Doc+Noc.
    /// </summary>
    public class Consultorios
    {
        //Ruta base del módulo.
        private readonly string controlador = "Consultorio";

        public async Task<ContenedorLista<DisponibilidadConsultorio>> TraeMisConsultoriosParaAPP(ParaFiltroUsuario content)
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<DisponibilidadConsultorio>)await api.PeticionHTTP<ContenedorLista<DisponibilidadConsultorio>>
                ($"{controlador}/TraeMisConsultoriosParaAPP", MetodoHTTP.Post, RequiereJwt.Si, content);
        }

        public async Task<ContenedorLista<EstadosCiudades>> TraeCiudadesConsultorio()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<EstadosCiudades>)await api.PeticionHTTP<ContenedorLista<EstadosCiudades>>
                ($"{controlador}/TraeCiudadesConsultorio", MetodoHTTP.Get, RequiereJwt.No);
        }
    }
}
