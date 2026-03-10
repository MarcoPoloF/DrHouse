using DocNoc.Models;
using DocNoc.Xam.Services;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API.Methods
{
    /// <summary>
    /// Contiene los métodos relacionados con las especialidades médicas registradas en Doc+Noc.
    /// </summary>
    public class Especialidades
    {
        //Ruta base del módulo.
        private readonly string controlador = "Especialidades";

        public async Task<ContenedorLista<Especialidad>> ListadoEspecialidades()
        {
            //Se genera la instancia de conexión API con TypeLocator.
            var api = TypeLocator.Instance.Resolve(typeof(DocNocAPI)) as DocNocAPI;

            //Se ejecuta la petición en la API.
            return (ContenedorLista<Especialidad>)await api.PeticionHTTP<ContenedorLista<Especialidad>>
                ($"{controlador}/ListadoEspecialidades", MetodoHTTP.Get, RequiereJwt.No);
        }
    }
}
