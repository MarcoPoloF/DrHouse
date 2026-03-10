using JsonNet.ContractResolvers;
using Newtonsoft.Json;
using PPS.Estandar;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PPS.ConexionAPI
{
    /// <summary>
    /// Clase estática empleada para realizar peticiones a APIs que siguen el estándar de PPS.
    /// </summary>
    public static class ApiPPS
    {
        /// <summary>
        /// Cliente HTTP para consultas a APIs de PPS.
        /// </summary>
        private static HttpClient ClienteHTTP = new HttpClient { MaxResponseContentBufferSize = 1000000 };

        /// <summary>
        /// Realiza consultas a APIs que se apegan al estándar de PPS, y procesa la respuesta.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dominioAPI"></param>
        /// <param name="rutaAPI"></param>
        /// <param name="metodoHTTP"></param>
        /// <param name="tipoRespuestaAPI"></param>
        /// <param name="requiereJWT"></param>
        /// <param name="contenido"></param>
        /// <returns></returns>
        public static async Task<IEntidad> PeticionHTTP<T>(string dominioAPI, string rutaAPI, MetodoHTTP metodoHTTP, RequiereJwt requiereJWT = RequiereJwt.No, string jwt = null, object contenido = null, bool esContenedor = false) where T : IEntidad
        {
            try
            {
                //Se genera la ruta de la petición a la API.
                string direccionConsulta = string.Format("{0}{1}", dominioAPI, rutaAPI);

                //Se declara el objeto que llevará el contenido.
                StringContent contenidoHTTP = new StringContent(String.Empty);

#if DEBUG
                Debug.WriteLine($"Solicitud API: {direccionConsulta}");
#endif

                //Se valida si se proporcionó contenido para la consulta.
                if (contenido != null)
                {
                    //System.Text.Json
                    ////El contenido se serializa a JSON.
                    //var modeloJSON = JsonSerializer.Serialize(contenido);

                    //Newtonsoft.Json
                    //El contenido se serializa a JSON.
                    var modeloJSON = JsonConvert.SerializeObject(contenido);

#if DEBUG
                    Debug.WriteLine($"JSON enviado a API: {modeloJSON}");
#endif

                    //Se carga el contenido al objeto de trabajo.
                    contenidoHTTP = new StringContent(modeloJSON, Encoding.UTF8, "application/json");
                }

                //Se elimina la cabecera "Authorization".
                if (ClienteHTTP.DefaultRequestHeaders.Contains("Authorization"))
                    ClienteHTTP.DefaultRequestHeaders.Remove("Authorization");

                //Se valida si la consulta requiere el envío del JWT.
                if (requiereJWT == RequiereJwt.Si)
                {
                    //Se valida la existencia del JWT en las propiedades de la App.
                    if (jwt == null)
                    {
                        //Se levanta una excepción si el JWT no fue proporcionado.
                        throw new Exception($"Conexión a '{rutaAPI}' requiere el envío de JWT.");
                    }

                    //Se agrega el JWT a la cabecera Authorization en el formato "Bearer {JWT}".
                    ClienteHTTP.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
                }

                //Se declara el objeto que almacenará la respuesta de la petición HTTP.
                HttpResponseMessage respuestaHTTP;

                //Se evalua el método HTTP a usar y se llama al método correspondiente.
                switch (metodoHTTP)
                {
                    case MetodoHTTP.Get:
                        respuestaHTTP = await ClienteHTTP.GetAsync(direccionConsulta);
                        break;
                    case MetodoHTTP.Post:
                        respuestaHTTP = await ClienteHTTP.PostAsync(direccionConsulta, contenidoHTTP);
                        break;
                    case MetodoHTTP.Put:
                        respuestaHTTP = await ClienteHTTP.PutAsync(direccionConsulta, contenidoHTTP);
                        break;
                    case MetodoHTTP.Delete:
                        respuestaHTTP = await ClienteHTTP.DeleteAsync(direccionConsulta);
                        break;
                    default:
                        throw new Exception("Excepción severa. Método HTTP no soportado.");
                }

                //Se valida que la respuesta HTTP tenga un código 200 (Ok).
                respuestaHTTP.EnsureSuccessStatusCode();

                //Se carga el contenido de la respuesta a una cadena de JSON.
                string respuestaJSON = await respuestaHTTP.Content.ReadAsStringAsync();

#if DEBUG
                Debug.WriteLine($"JSON recibido de API: {respuestaJSON}");
#endif

                //System.Text.Json
                ////Se instancia el objeto de opciones JSON, y se activa la opción de ignorar mayúsculas/minúsculas en los nombres de propiedad.
                //var opcionesJSON = new JsonSerializerOptions()
                //{
                //    PropertyNameCaseInsensitive = true
                //};

                //System.Text.Json
                ////Se dserializa la respuesta al tipo de objeto solicitado.
                //T resultado = JsonSerializer.Deserialize<T>(respuestaJSON, opcionesJSON);

                if (esContenedor)
                {
                    var opcionesJSON = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    opcionesJSON.Converters.Add(new ContenedorTConverter());
                    var resp =  System.Text.Json.JsonSerializer.Deserialize<T>(respuestaJSON, opcionesJSON);
                    return resp;
                }

                //Newtonsoft.Json + JsonNet.ContractResolvers
                ////Se instancia el objeto de opciones JSON, para procesar los setters privados de IEntidad.
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new PrivateSetterContractResolver()
                };

                //Newtonsoft.Json
                ////Se deserializa la respuesta al tipo de objeto solicitado.
                T resultado = JsonConvert.DeserializeObject<T>(respuestaJSON, jsonSettings);

                //Se devuelve el objeto solicitado
                return resultado;
            }
            catch (Exception ex)
            {
                var resultadoError = new Entidad($"Excepción en ruta {rutaAPI}. {ex.Message}", true);

                var errorJSON = JsonConvert.SerializeObject(resultadoError);

                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new PrivateSetterContractResolver()
                };
                T resultado = JsonConvert.DeserializeObject<T>(errorJSON, jsonSettings);

                return resultado;
            }
        }


    }
}
