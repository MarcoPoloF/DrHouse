using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la información de aseguradoras registradas en Doc+Noc.
    /// </summary>
    public class Aseguradoras
    {
        //Ruta base del módulo.
        private readonly string controlador = "Aseguradoras";

        public async Task<ContenedorLista<Aseguradora>> ListadoAseguradoras()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<Aseguradora>)await api.PeticionHTTP<ContenedorLista<Aseguradora>>
                ($"{controlador}/ListadoAseguradoras", MetodoHTTP.Get, RequiereJwt.No);
        }
    }
}
