using DocNoc.Xam.Interfaces;
using PPS.ConexionAPI;
using PPS.Estandar;
using System.Threading.Tasks;

namespace DocNoc.API
{
    /// <summary>
    /// Clase empleada para realizar peticiones a la API de Doc+Noc.
    /// </summary>
    public class DocNocAPI
    {
        protected string jwt;

        protected string rutaApi;

        public DocNocAPI(IPreferenceService preferenceService)
        {
            //rutaApi = "https://docnocqaapi.azurewebsites.net/api/";
            rutaApi = "https://housedevqa.azurewebsites.net/api/";
            jwt = preferenceService.Get("jwt");
        }

        public async Task<IEntidad> PeticionHTTP<T>(string rutaAPI, MetodoHTTP metodoHTTP,
            RequiereJwt requiereJWT = RequiereJwt.No, object contenido = null, bool esContenedor = false) where T : IEntidad
        {
            string token = null;

            if (requiereJWT == RequiereJwt.Si)
                token = jwt;

            return await ApiPPS.PeticionHTTP<T>(rutaApi, rutaAPI, metodoHTTP, requiereJWT, token, contenido, esContenedor);
        }
    }
}
