using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con la información de pacientes registrados en Doc+Noc.
    /// </summary>
    public class TipoEvento
    {
        //Ruta base del módulo.
        private readonly string controlador = "TipoEvento";

        public async Task<ContenedorLista<TiposEventos>> Lista()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;
            //Se ejecuta la petición en la API.
            return (ContenedorLista<TiposEventos>)await api.PeticionHTTP<ContenedorLista<TiposEventos>>
                ($"{controlador}/Lista", MetodoHTTP.Get);
        }
    }
}
